using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class FactionData {

    private string factionName;
    private string factionDescription;

    private int id;
    private static int lastId;

    private Character leader;
    private List<Character> members;
    private List<BandData> bands;

    private static int maxMembers;
    private static int maxBands;

    private bool isEmpty;

    public FactionData( ) {

        factionName = "factionName";
        factionDescription = "factionDescription";
        
        leader = null;
        members = new List<Character>();
        bands = new List<BandData>();

        maxMembers = 10;
        maxBands = 10;

        InitializeBands();

        isEmpty = true;

    }

    /// <summary>
    /// Initialize bands. Bands id's are declared as factionId + i: faction 0 => bands = 00, 01, 02, ... ; faction 1 => bands = 10, 11, 12, ...
    /// </summary>
    public void InitializeBands( ) {

        for ( int i = 0; i < maxBands; i++ ) {

            string idAux = i.ToString();
            idAux = idAux + id;

            bands.Add( new BandData() );
            bands[i].SetBandId( int.Parse( idAux ) );

        }

    }

    public void AddNewId( ) {

        id = lastId++;
        lastId= id;
    
    }

    public void SetFactionId( int id ) {
        
        this.id = id;
    
    }

    public int GetFactionId( ) {

        return id;
    
    }

    public void SetLeader( Character leader ) {

        this.leader = leader;
    
    }

    public Character GetLeader() {

        return leader; 
    
    }

    public void SetFactionName(string factionName ) {
    
        this.factionName = factionName;
    
    }

    public string GetFactionName() {
    
        return factionName;
    
    }

    public void SetDescription( string description ) {
        
        factionDescription= description;

    }

    public string GetDescription( ) {

        return factionDescription;
    
    }

    public BandData GetBandData( int id ) {

        for ( int i = 0; i < bands.Count; i++ ) {

            if ( bands[i].GetBandId() == id ) {

                return bands[id];
            
            }

        }

        return null;
    
    }

    public bool IsFactionEmpty( ) {

        return isEmpty;

    }

    public void AddMember( Character character, string typeOfEntity ) {

        if ( members.Count < maxMembers ) {

            if ( members != null && !members.Contains( character )) {

                members.Add( character );
                character.SetFactionId( id );
                character.SetInAFaction( true );
                character.tag = typeOfEntity;

            }
            else {

                Debug.Log( "El tipo este ya es de la facción." );

            }

        }
        else {

            Debug.Log("Se ha superado el máximo de miembros.");
        
        }
    
    }

    public void RemoveMember( Character character, string typeOfEntity ) {

        // Check if its trying to remove player from the default player faction.
        if ( id == 0 && character.GetId() == 0 ) {

            Debug.Log( "No se puede eliminar al jugador de su facción." );

        }
        else {

            if ( members != null && members.Contains( character ) ) {

                members.Remove( character );
                character.SetFactionId( 0 );
                character.SetInAFaction( false );
                character.tag = typeOfEntity;

            }
            else {

                Debug.Log( "El tipo este no está en la facción." );

            }

        }

    }

    public void AddBand( BandData band ) {

        if ( bands.Count < maxBands ) {

            if ( bands != null && bands.Contains( band )! ) {

                bands.Add( band );

            }
            else {

                Debug.Log( "El tipo este ya es de la facción." );

            }

        }
        else {

            Debug.Log("Se ha superado el número de grupos.");
        
        }

    
    }

    public void RemoveBand( BandData bandData ) {

        // Check if its trying to remove player from the default player faction.
        if ( id == 0 && bandData.GetBandId() == 0 ) {

            Debug.Log( "No se puede eliminar al jugador de su facción." );

        }
        else {

            if ( bands != null && bands.Contains( bandData ) ) {

                bands.Remove( bandData );

            }
            else {

                Debug.Log( "El tipo este no está en la facción." );

            }

        }

    }

}
