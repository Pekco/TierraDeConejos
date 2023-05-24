
using UnityEngine;

public class BodyPart : MonoBehaviour{

    private int maxVitalidad;

    public int vitalidad { get; private set; }

    public int armadura { get; private set; }

    public bool cortado { get; private set; }

    public bool sangrado { get; private set; }

    public bool quemado { get; private set; }

    public bool amputado { get; private set; }

    public delegate void OnPlayerHealthChanged( float health );
    public event OnPlayerHealthChanged onPlayerHealthChanged;

    private void Start ( ) {
        
        maxVitalidad = 100;
        vitalidad = maxVitalidad;
        armadura = 0;
    
    }

    public void TakeDamage( int damage ) {

        vitalidad -= damage;

        if ( onPlayerHealthChanged != null ) {

            onPlayerHealthChanged?.Invoke( vitalidad/maxVitalidad );
        
        }
    
    }

    public void Heal( int amount ) {

        vitalidad += amount;
    
    }

    public void Cangrenar() {
        
        
    
    }

    public void Amputar( ) {
    
    
    
    }

}
