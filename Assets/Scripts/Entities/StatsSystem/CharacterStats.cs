using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [SerializeField] Body body;

    public PrimaryStat nivel;
    public PrimaryStat experiencia;

    List<string> claves;

    private void Start( ) {

        claves = new List<string>(body.bodyParts.Keys);

    }

    private void Update( ) {

        if ( Input.GetKeyDown( KeyCode.L ) ) {

            string clave = claves[Random.Range( 0, body.bodyParts.Count )];

            BodyPart part = body.bodyParts[clave];
            part.TakeDamage( Random.Range(0, 10) );

        }

    }

}
