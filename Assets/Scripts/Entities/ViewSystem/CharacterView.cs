using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour {

    public float viewRadius = 5f;
    [Range( 0f, 360f )]
    public float viewAngle = 67.5f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [SerializeField] private SpriteMask spriteMask;
    private Transform maskTransform;

    private Entity entity;

    float adjustedViewAngle;

    private void Start( ) {

        entity = GetComponent<Entity>();
        maskTransform = spriteMask.GetComponent<Transform>();

    }

    private void OnDrawGizmosSelected( ) { 

        if ( !Application.isPlaying ) {

            return;
             
        }

        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireSphere( transform.position, viewRadius );

        Vector3 viewAngleA = CalculateDirectionFromAngle( adjustedViewAngle + viewAngle );
        Vector3 viewAngleB = CalculateDirectionFromAngle( adjustedViewAngle - viewAngle );

        Gizmos.color = Color.red;
        Gizmos.DrawLine( transform.position, transform.position + viewAngleA * viewRadius );
        Gizmos.DrawLine( transform.position, transform.position + viewAngleB * viewRadius );

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine( transform.position, transform.position + entity.GetDirection() * viewRadius );
    }

    private void Update( ) {

        // Calcular el ángulo de visión basado en el movimiento
        float angle = CalculateAngleFromDirection( entity.GetDirection() );
        adjustedViewAngle = angle;

        maskTransform.eulerAngles = new Vector3( 0.0f, 0.0f, angle + 90 );

        // Obtener todos los objetos en el radio de visión
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll( transform.position, viewRadius, targetMask );

        foreach ( Collider2D targetCollider in targetsInViewRadius ) {

            // Calcular el ángulo entre el objeto y el campo de visión
            Vector2 directionToTarget = (targetCollider.transform.position - transform.position).normalized;
            //float angleToTarget = Vector2.Angle( entity.GetDirection(), directionToTarget );
            float angleToTarget = CalculateAngleFromDirection( directionToTarget );

            if ( targetCollider.transform != transform) {

                // Comprobar
                if ( angleToTarget < adjustedViewAngle + viewAngle && angleToTarget > adjustedViewAngle - viewAngle ) {

                    if ( !Physics2D.Raycast( transform.position, directionToTarget, Vector2.Distance( transform.position, targetCollider.transform.position ), obstacleMask ) ) {

                        ShowEntity( targetCollider, true );
                        ShowBlur( targetCollider, false );

                    }
                    else {

                        ShowEntity( targetCollider, false );
                        ShowBlur( targetCollider, true );

                    }

                }
                else {

                    ShowEntity( targetCollider, false );
                    ShowBlur( targetCollider, true );

                }

            }

        }

    }

    private void ShowEntity( Collider2D targetCollider, bool show ) {

        SpriteRenderer[] spriteRenderers = targetCollider.GetComponentsInChildren<SpriteRenderer>();
        foreach ( SpriteRenderer spriteRenderer in spriteRenderers ) {
            spriteRenderer.enabled = show;
        }

    }

    private void ShowBlur( Collider2D targetCollider, bool show ) {

        Transform blur = targetCollider.transform.Find("Blur");
        
        blur.gameObject.SetActive( show );

    }

    private Vector2 CalculateDirectionFromAngle( float angle ) {

        float angleInRadians = angle * Mathf.Deg2Rad;


        float x = Mathf.Cos( angleInRadians );
        float y = Mathf.Sin( angleInRadians );


        return new Vector2( x, y );
    }

    private float CalculateAngleFromDirection( Vector3 direction ) {

        float angleOffset = Mathf.Atan2( direction.y, direction.x ) * Mathf.Rad2Deg;

        return angleOffset;

    }
    
}
