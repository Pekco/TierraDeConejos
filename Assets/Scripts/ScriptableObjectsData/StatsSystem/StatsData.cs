using UnityEngine;

[System.Serializable]
public class PrimaryStat {

    public int value {

        get {

            return value; 
        
        }
        private set {
        
            Mathf.Clamp(value, 0, 50);
        
        }
    
    }

    public int GetValue( ) {
    
        return value;
    
    }

    public void SetValue( int value ) {
    
        this.value = value;
    
    }

    public void LevelUp( ) {

        value++;
    
    }

}

public class SecondaryStat {

    public int value {

        get {

            return value;

        }
        private set {

            Mathf.Clamp( value, 0, 200 );

        }

    }

    public int GetValue( ) {

        return value;

    }

    public void SetValue( int value ) {

        this.value = value;

    }

    public void LevelUp( ) {

        value++;

    }

}

public class ProfessionStat {

    public int value {

        get {

            return value;

        }
        private set {

            Mathf.Clamp( value, 0, 10 );

        }

    }

    public int GetValue( ) {

        return value;

    }

    public void SetValue( int value ) {

        this.value = value;

    }

    public void LevelUp( ) {

        value++;

    }

}


public class StatsData : ScriptableObject {

    public int nivel = 0;

    public float vitalidadMax = 100.0f;

    public float expMax = 100.0f;
    public float multiplicadorExp = 1.1f;

    // Stats primarios. Cada nivel otorga 5 puntos.

    public int fuerza = 0;
    public int destreza = 0;
    public int constitucion = 0;
    public int inteligencia = 0;
    public int sabiduría = 0;
    public int elocuencia = 0;
    public int percepcion = 0;

    // Stats secundarios. Se suben haciendo lo  que toca o subiendo los stats primarios.

    // Asesino:
    public int sigilo = 0;
    public int robo = 0;
    public int asesinato = 0;
    public int forcejeo = 0;

    // Luchador:
    public int ataqueMelee = 0;
    public int ataqueDistancia = 0;
    public int ataqueArtesMarciales = 0;
    public int precisión = 0;
    public int defensa = 0;
    public int bloqueo = 0;
    public int esquiva = 0;

    // Ciencia:
    public int ciencia = 0;
    public int ingeniería = 0;
    public int medicina = 0;



    // Oficios. Se sube alcanzando ciertas condiciones de stats primarios, secundarios y libros, revistas o profesores.

    public int carpinteria = 0;
    public int zapateria = 0;
    public int sastreria = 0;
    public int herreria = 0;
    public int robotica = 0;
    public int computacion = 0;
    public int investigacion = 0;
    public int cocina = 0;
    public int mecanica = 0;
    public int primerosAuxilios = 0;
    public int agricultura = 0;
    public int adiestramiento = 0;
    public int enseñanza = 0;

}
