using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( Rigidbody2D ) )]
[RequireComponent( typeof( CapsuleCollider2D ) )]
[RequireComponent( typeof( Animator ) )]

public class CharacterControllerAI : MonoBehaviour {

    private NavMeshAgent agent;
    Animator bodyAnimator;
    Animator equipmentAnimator;
    private Character character;
    private StateModeBehaviour stateMode;
    private Rigidbody2D rb2d;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float combatSpeed;

    private Vector2 lastMotionVector;
    private Vector3 direction;

    [SerializeField] private Transform targetTransform;
    [SerializeField] private Character targetCharacter;
    [SerializeField] private StateModeBehaviour targetStateMode;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float followRadius = 2.0f;
    [SerializeField] private Vector3 localPos;
    [SerializeField] private Vector3 localPosWithOffSet;
    [SerializeField] private Vector3 worldPosWithOffSet;
    [SerializeField] private Vector3 x, y;

    [SerializeField] private int bandPosition;
    [SerializeField] private Vector3 bandPositionInMatrixPanel;

    [SerializeField] private BandData band;
    [SerializeField] private BandModes bandModes;

    public LayerMask groundLayer, targetLayer;

    private Coroutine destinationStateCoroutine;

    public delegate void SetDestinationToTarget( );
    public event SetDestinationToTarget setDestinationToTarget;

    private bool follow;

    private void Awake( ) {

        bodyAnimator = gameObject.transform.Find( "Joey_Sprite" ).GetComponent<Animator>();
        equipmentAnimator = gameObject.transform.Find( "Equipo" ).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
        stateMode = GetComponent<StateModeBehaviour>();
        rb2d = GetComponent<Rigidbody2D>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        follow = false;

        worldPosWithOffSet = Vector3.zero;
        localPosWithOffSet = Vector3.zero;
        x = Vector3.zero;
        y = Vector3.zero;

    }

    private void Start( ) {

        walkSpeed = 100.0f;
        runSpeed = 150.0f;
        combatSpeed = 65.0f;

        targetTransform = gameObject.transform;

        bandPosition = -1;

        if ( character.IsInABand() ) {

            AddBandActions();
        
        }

        setDestinationToTarget += SetBandLeaderAsTarget;

        stateMode.ChangeMode( ModeList.Walk );
        SetMode(stateMode.Mode);

    }

    private void Update() {

        Vector2Int motionVector = Vector2Int.zero;
        motionVector.x = ( int ) Mathf.Clamp( agent.velocity.x, -1, 1 );
        motionVector.y = ( int ) Mathf.Clamp( agent.velocity.y, -1, 1 );

        bodyAnimator.SetBool( "moving", agent.velocity.x != 0 || agent.velocity.y != 0 );
        bodyAnimator.SetFloat( "horizontal", motionVector.x );
        bodyAnimator.SetFloat( "vertical", motionVector.y );

        equipmentAnimator.SetBool( "moving", agent.velocity.x != 0 || agent.velocity.y != 0 );
        equipmentAnimator.SetFloat( "horizontal", motionVector.x );
        equipmentAnimator.SetFloat( "vertical", motionVector.y );

        LastDirection( motionVector.x, motionVector.y );

        if ( follow ) {

            SetLocalTargetPosToWorld();
            worldPosWithOffSet = targetTransform.position + localPosWithOffSet;
            agent.SetDestination( worldPosWithOffSet );


        }

        if ( transform.position == agent.pathEndPosition) {
        
            rb2d.velocity = Vector3.zero;
        
        }


    }

    private void OnEnable( ) {

        if( character.IsInABand() ) {

            AddBandActions();

        }
        else if ( setDestinationToTarget == null) {

            setDestinationToTarget += SetBandLeaderAsTarget;
        
        }

    }

    private void OnDisable( ) {

        if ( character.IsInABand() ) {

            RemoveBandActions();

        }
        else if ( setDestinationToTarget != null ) {

            setDestinationToTarget = null;
        
        }

    }

    public void SetBandPos( int pos ) {

        bandPosition = pos;
    
    }

    public void SetBand( BandData bandData ) {

        if ( bandData == null ) {

            RemoveBandActions();
            band = null;

        }
        else {

            band = bandData;
            AddBandActions();

        }

    }

    private void LastDirection( float horizontal, float vertical ) {
         
        if ( stateMode.Mode == ModeList.Combat || follow && targetCharacter != null ) {

            lastMotionVector = direction;

            equipmentAnimator.SetFloat( "lastHorizontal", direction.x );
            equipmentAnimator.SetFloat( "lastVertical", direction.y );

            bodyAnimator.SetFloat( "lastHorizontal", direction.x );
            bodyAnimator.SetFloat( "lastVertical", direction.y );

        }
        else if ( IsMoving() | stateMode.Mode == ModeList.Combat ) {

            lastMotionVector = new Vector2(
                horizontal,
                vertical
                ).normalized;

            equipmentAnimator.SetFloat( "lastHorizontal", horizontal );
            equipmentAnimator.SetFloat( "lastVertical", vertical );

            bodyAnimator.SetFloat( "lastHorizontal", horizontal );
            bodyAnimator.SetFloat( "lastVertical", vertical );

        }

    }

    public void SetTarget( Transform target ) {

        this.targetTransform = target;
        targetCharacter = target.GetComponent<Character>();
        
        if ( transform != target ) {

            targetStateMode = target.GetComponent<StateModeBehaviour>();
            targetStateMode.OnModeChange += ChangeMode;

        }
        else {

            targetStateMode.OnModeChange -= ChangeMode;

        }

    }

    public void SetPosition( Vector3 pos ) {

        agent.SetDestination( pos );
    
    }

    public void SetBandLeaderAsTarget( ) {

        localPos = bandModes.GetAngFromMode( band.GetBandMode(), bandPosition, band.GetMemberCount() );

        follow = true;

    }

    private void AddBandActions( ) {

        band.ChangeBandModeAction += SetBandLeaderAsTarget;
        band.StandStillAllAllies += StandStill;
        band.BandSpeed += SetBandSpeed;

    }

    private void RemoveBandActions( ) {

        band.ChangeBandModeAction -= SetBandLeaderAsTarget;
        band.StandStillAllAllies -= StandStill;
        band.BandSpeed -= SetBandSpeed;

    }

    /// <summary>
    /// Esto podría servir para decidir si se va a un objetivo que se mueve ( aliado, enemigo, etc. ) o algún objetivo estático ( para recoger un item, trabajar en una mesa, etc. ).
    /// </summary>
    /// <param name="oldState"></param>
    /// <param name="newState"></param>
    private void ChangeMode( ModeList oldMode, ModeList newMode ) {

        SetMode(newMode);
    
    }

    private void SetMode( ModeList newMode ) {

        if ( destinationStateCoroutine != null ) {

            StopCoroutine( destinationStateCoroutine );

        }

        switch ( newMode ) {

            case ModeList.Combat:

                destinationStateCoroutine = StartCoroutine(CombatMode(newMode));            ;
                
                break;

            case ModeList.Walk:

                WalkMode();

                break;

            case ModeList.Run:

                RunMode();

                break;
        
        }
    
    }

    private void ChangeToCombatMode( ) {

        bodyAnimator.SetBool( "combatMode", true );
        equipmentAnimator.SetBool( "combatMode", true );
        agent.speed = combatSpeed;

    }

    private IEnumerator CombatMode( ModeList newMode ) {

        while ( targetStateMode.Mode == ModeList.Combat ) {

            if ( agent.remainingDistance > agent.stoppingDistance + 50.0f ) {

                RunMode();

            }

            yield return new WaitUntil( ( ) => agent.remainingDistance <= agent.stoppingDistance + 50.0f );

            bodyAnimator.SetBool( "combatMode", true );
            equipmentAnimator.SetBool( "combatMode", true );
            agent.speed = combatSpeed;
            stateMode.ChangeMode( newMode );

            yield return new WaitUntil( ( ) => agent.remainingDistance > agent.stoppingDistance + 50.0f );

        }

    }

    private void WalkMode( ) {

        bodyAnimator.SetBool( "combatMode", false );
        equipmentAnimator.SetBool( "combatMode", false );
        agent.speed = walkSpeed;

    }

    private void RunMode( ) {

        bodyAnimator.SetBool( "combatMode", false );
        equipmentAnimator.SetBool( "combatMode", false );
        agent.speed = runSpeed;

    }

    private void SetLocalTargetPosToWorld( ) {

        direction = targetCharacter.GetDirection();

        x[0] = direction[1];
        x[1] = - direction[0];

        y[0] = direction[0];
        y[1] = direction[1];

        int xF = ( int ) ( bandModes.offSet * localPos[0] );
        int yF = ( int ) ( bandModes.offSet * localPos[1] );
        
        localPosWithOffSet[0] = x[0] * xF + y[0] * yF;
        localPosWithOffSet[1] = x[1] * xF + y[1] * yF;

    }

    /// <summary>
    /// If bandleader is deciding an order, the ally is able to stand near the leader meanwhile.
    /// </summary>
    /// <param name="localPos"></param>
    public void SetLocalPosToWaitOrders( Vector2 localPos ) {

        this.localPos = localPos;
    
    }

    public void StandStill( bool stand ) {

        if ( stand ) {

            follow = false;
            SetPosition( transform.position );

        }
        else {

            follow = true;

        }
    
    }

    public void SetBandSpeed( bool speed ) {

        if ( speed ) {

            agent.speed = band.GetBandSpeed();

        }
        else {

            SetMode( stateMode.Mode );
        
        }

    }

    public float GetSpeed( ) {

        return agent.speed;
    
    }

    public bool IsMoving( ) {

        return agent.velocity != Vector3.zero;
    
    }

}
