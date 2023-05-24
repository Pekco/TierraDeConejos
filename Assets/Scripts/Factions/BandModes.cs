using UnityEngine;

public enum bandModes {

    Pelotón,
    Fila,
    Exploración,
    Avanzadilla,
    Escudo,
    Aleatorio

}

[CreateAssetMenu( menuName = "Data/Band Modes" )]
public class BandModes : ScriptableObject {

    [SerializeField] public int offSet;
    [SerializeField] public const int maxBandMembers = 9;

    // Posición del jugador = { 0, 0 }, dirección del jugador - Norte. Utilizar la matriz solo como guia.
    public readonly int[,] zoneMatrix = new int[81,2] {

        { -4,  4 }, { -3,  4 }, { -2,  4 }, { -1,  4 }, { 0,  4 }, { 1,  4 }, { 2,  4 }, { 3,  4 }, { 4,  4 },
        { -4,  3 }, { -3,  3 }, { -2,  3 }, { -1,  3 }, { 0,  3 }, { 1,  3 }, { 2,  3 }, { 3,  3 }, { 4,  3 },
        { -4,  2 }, { -3,  2 }, { -2,  2 }, { -1,  2 }, { 0,  2 }, { 1,  2 }, { 2,  2 }, { 3,  2 }, { 4,  2 },
        { -4,  1 }, { -3,  1 }, { -2,  1 }, { -1,  1 }, { 0,  1 }, { 1,  1 }, { 2,  1 }, { 3,  1 }, { 4,  1 },
        { -4,  0 }, { -3,  0 }, { -2,  0 }, { -1,  0 }, { 0,  0 }, { 1,  0 }, { 2,  0 }, { 3,  0 }, { 4,  0 },
        { -4, -1 }, { -3, -1 }, { -2, -1 }, { -1, -1 }, { 0, -1 }, { 1, -1 }, { 2, -1 }, { 3, -1 }, { 4, -1 },
        { -4, -2 }, { -3, -2 }, { -2, -2 }, { -1, -2 }, { 0, -2 }, { 1, -2 }, { 2, -2 }, { 3, -2 }, { 4, -2 },
        { -4, -3 }, { -3, -3 }, { -2, -3 }, { -1, -3 }, { 0, -3 }, { 1, -3 }, { 2, -3 }, { 3, -3 }, { 4, -3 },
        { -4, -4 }, { -3, -4 }, { -2, -4 }, { -1, -4 }, { 0, -4 }, { 1, -4 }, { 2, -4 }, { 3, -4 }, { 4, -4 }

    };

    public void Init( ) {
    
        offSet = 50;
        //localPos = Vector3.zero;

    }

    public Vector3 GetAngFromMode( bandModes bandMode, int posInBand, int memberCount ) {

        Vector3 localPos = new Vector3( 0, 0, 0 );

        switch ( bandMode ) {

            case bandModes.Pelotón:
                
                localPos = Peloton( posInBand, memberCount );

                break;

            case bandModes.Fila:

                localPos = Fila( posInBand, memberCount );

                break;

            case bandModes.Exploración:



                break;

            case bandModes.Avanzadilla: 



                break;

            case bandModes.Escudo:



                break;

            case bandModes.Aleatorio:


                break;

        }

        return localPos;
    
    }

    /// <summary>
    /// This will position the allies in a square form behind the leader, except if there are two members.
    /// </summary>
    /// <param name="posInBand"></param>
    /// <param name="memberCount"></param>
    public Vector3 Peloton( int posInBand, int memberCount ) {

        Vector3 localPos = Vector3.zero;

        if ( memberCount == 1 ) {

            localPos[0] = 0;
            localPos[1] = -1;

        }
        else if ( memberCount == 2 ) {

            if ( posInBand == 1 ) {

                localPos[0] = 1;
                localPos[1] = -1;

            }
            else {

                localPos[0] = -1;
                localPos[1] = -1;

            }

        }
        else {

            int sideNumOfPeloton = 3; // The number of allies that has the square + 1, based on the quantity of band members.

            localPos[1] = Mathf.CeilToInt( (float) posInBand / (float) sideNumOfPeloton ); // y == deep. In case memberCount == 9, the deep could be -1, -2, -3.
            localPos[1] = - localPos[1];
            
            localPos[0] = ( ( posInBand - 1 ) % ( sideNumOfPeloton ) );
            localPos[0] = ( localPos[0] ) * ( Mathf.PI / 2 );
            localPos[0] = Mathf.Cos( localPos[0] );

            localPos[2] = 0;

        }

        return localPos;

    }

    public Vector3 Fila( int posInBand, int memberCount ) {

        Vector3 localPos = Vector3.zero;

        localPos[1] = -1 * posInBand;

        return localPos;
    
    }

}

