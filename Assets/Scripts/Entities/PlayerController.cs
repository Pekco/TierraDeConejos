using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( Rigidbody2D ) )]

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb2d;
    Animator bodyAnimator;
    Animator equipmentAnimator;
    Camera playerCamera;
    Character character;
    StateModeBehaviour stateMode;
    NavMeshAgent agent;

    [SerializeField] float speed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float combatSpeed;

    Vector2 motionVector;
    Vector2 lastMotionVector;
    Vector3 rawDirection;
    Vector3 direction;
    float distanceToMouse;

    private void Awake( ) {

        rb2d = GetComponent<Rigidbody2D>();
        bodyAnimator = gameObject.transform.Find( "Equipo" ).GetComponent<Animator>();
        equipmentAnimator = gameObject.transform.Find( "Joey_Sprite" ).GetComponent<Animator>();
        playerCamera = Camera.main;
        character = GetComponent<Character>();
        stateMode = GetComponent<StateModeBehaviour>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start() {

        speed = 100.0f;
        walkSpeed = 80000.0f;
        runSpeed = 16000.0f;
        combatSpeed = 50000.0f;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        stateMode.ChangeMode(ModeList.Walk);
        speed = walkSpeed;

    }

    private void Update( ) {

        float horizontal = Input.GetAxisRaw( "Horizontal" );
        float vertical = Input.GetAxisRaw( "Vertical" );

        motionVector = new Vector2(
            horizontal,
            vertical
            );

        ChangeTheState();
        ChangeTheMode();
        SetAnimationStateBools();
        SetAnimationModeBools();

    }

    void FixedUpdate( ) {

        if ( IsMoving() ) {

            Move();

        }
        else {

            rb2d.velocity = Vector3.zero;
        
        }

    }

    private void CombatWalk( ) {

        float horizontal = Input.mousePosition.x;
        float vertical = Input.mousePosition.y;

        rawDirection = playerCamera.ScreenToWorldPoint( new Vector3( horizontal, vertical, 0 ) );

        bodyAnimator.SetFloat( "horizontalMouse", direction.x );
        bodyAnimator.SetFloat( "verticalMouse", direction.y );

        equipmentAnimator.SetFloat( "horizontalMouse", direction.x );
        equipmentAnimator.SetFloat( "verticalMouse", direction.y );

        CalculateDirection();

        LastMotion(direction.x, direction.y);

    }

    private void LastMotion( float horizontal, float vertical ) {

        if ( IsMoving() | stateMode.Mode == ModeList.Combat) {

            lastMotionVector = new Vector2(
                horizontal,
                vertical
                ).normalized;

            bodyAnimator.SetFloat( "lastHorizontal", horizontal );
            bodyAnimator.SetFloat( "lastVertical", vertical );

            equipmentAnimator.SetFloat( "lastHorizontal", horizontal );
            equipmentAnimator.SetFloat( "lastVertical", vertical );

        }

        direction = new Vector3( lastMotionVector.x, lastMotionVector.y, 0 );

    }

    private void ChangeTheMode( ) {

        if ( Input.GetMouseButtonDown( 1 ) ) {

            speed = combatSpeed;
            stateMode.ChangeMode( ModeList.Combat );

        }
        else if ( Input.GetKeyDown( KeyCode.LeftShift ) ) {

            speed = runSpeed;
            stateMode.ChangeMode( ModeList.Run );

        }
        else if ( Input.GetMouseButtonUp( 1 ) || Input.GetKeyUp( KeyCode.LeftShift ) ) {

            speed = walkSpeed;
            stateMode.ChangeMode( ModeList.Walk );

        }

    }

    private void CalculateDirection( ) {

        rawDirection = rawDirection - transform.position; // Dirección sin normalizar.
        distanceToMouse = rawDirection.magnitude;
        direction = rawDirection / distanceToMouse; // Dirección normalizada.

        character.SetDirection( direction );

    }

    private void Move( ) {

        Vector2 force = motionVector * speed * Time.fixedDeltaTime;

        rb2d.AddForce( force, ForceMode2D.Impulse );

        Vector2 vel = rb2d.velocity;

        vel.x = Mathf.Clamp( vel.x, -100, 100 );
        vel.y = Mathf.Clamp( vel.y, -100, 100 );

        rb2d.velocity = vel;

    }

    private void Stop( ) {

        //rb2d.AddForce( -rb2d.velocity * deceleracion * Time.fixedDeltaTime, ForceMode2D.Impulse );
        agent.velocity = Vector3.zero;

    }

    public Vector3 GetDirection( ) {

        return direction;
    
    }

    private bool IsMoving( ) {

        return  motionVector != Vector2.zero;

    }

    private void ChangeTheState( ) {

        if (  motionVector != Vector2.zero ) {

            stateMode.ChangeState( StateList.Moving );

        }
        else {

            stateMode.ChangeState( StateList.Idle );

        }

    }

    private void SetAnimationStateBools( ) {

        bodyAnimator.SetBool( "moving", IsMoving() );
        bodyAnimator.SetFloat( "horizontal", motionVector[0] );
        bodyAnimator.SetFloat( "vertical", motionVector[1] );

        equipmentAnimator.SetBool( "moving", IsMoving() );
        equipmentAnimator.SetFloat( "horizontal", motionVector[0] );
        equipmentAnimator.SetFloat( "vertical", motionVector[1] );

    }

    private void SetAnimationModeBools( ) {

        if ( stateMode.Mode == ModeList.Combat ) {

            bodyAnimator.SetBool( "combatMode", true );
            equipmentAnimator.SetBool( "combatMode", true );

            CombatWalk();

        }
        else {

            bodyAnimator.SetBool( "combatMode", false );
            equipmentAnimator.SetBool( "combatMode", false );

            LastMotion( motionVector.x, motionVector.y );

            character.SetDirection( direction );

        }

    }

}
