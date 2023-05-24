using UnityEngine;

public class FactionsController : MonoBehaviour {

    public FactionsList factionsList;
    public int playerFactionId;

    [SerializeField] private NPCMenuOptionDataContainer optionData;

    public void Awake( ) {

        playerFactionId = 0;
        factionsList = new FactionsList();
        SetDefaultPlayerFactionAndBand( playerFactionId );

    }

    public void Start() {
    
    }

    /// <summary>
    /// Fills the lists with empty information, just for optimization.
    /// </summary>
    public void InitializeFactionAndBandsLists( ) {

        factionsList.InitializeFactionsAndBands();
    
    }

    public void SetDefaultPlayerFactionAndBand( int id ) {

        
        Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player.SetFactionId( id );
        player.SetBandId( id );
        player.SetInAFaction( true );
        player.SetInABand( true );

        factionsList.AddFaction(new FactionData());

        if ( factionsList != null ) {

            FactionData playerFaction = factionsList.GetFactionData( 0 );
            if ( playerFaction != null ) {

                playerFaction.SetLeader( player );
                playerFaction.GetBandData( 0 ).SetLeader( player );

            }
            else {

                Debug.Log( "ERROR: Null pointer exception, bitch. No se ha creado la lista de facciones antes de asignar la facción por defecto ( id = 0 ) al jugador." );
            
            }

        }
        else {

            Debug.Log( "ERROR: Null pointer exception, bitch. No se ha creado el contenedor de facciones antes de asignar la facción por defecto ( id = 0 ) al jugador." );
        
        }

    }

    public FactionData GetFactionData( int factionId ) {
    
        return factionsList.GetFactionData( factionId );
    
    }

    public BandData GetBandData( int factionId, int BandId ) {
    
        return factionsList.GetFactionData( factionId ).GetBandData( BandId );

    }

    public Character GetBandLeaderFromAFaction(int factionId, int bandId) {

        FactionData factionData = factionsList.GetFactionData( factionId );
        if( factionData != null ) {

            BandData bandData = factionData.GetBandData( bandId );
            if( bandData != null ) {

                return bandData.GetLeader();

            }

            return null;

        }
        
        return null;

    }

    public Character GetFactionLeader( int factionId ) {

        FactionData factionData = factionsList.GetFactionData( factionId );
        if( factionData != null ) {

            return factionData.GetLeader();

        }

        return null;

    }

    public void AddMemberToFaction( Character newMember, int factionId) {

        if ( newMember.IsInAFaction() ) {

            Debug.Log( "Ya está en una facción." );
            return;
        
        }

        FactionData factionData = factionsList.GetFactionData( factionId );
        if ( factionData != null ) {

            factionData.AddMember( newMember, optionData.typeOfEntity[2] );

        }
        else {

            Debug.Log( "La facción no existe." );
        
        }
    
    }

    public void RemoveMemberFromFaction( Character member, int factionId ) {

        if ( !member.IsInAFaction() ) {

            Debug.Log( "No está en ninguna facción." );
            return;

        }

        FactionData factionData = factionsList.GetFactionData( factionId );
        if ( factionData != null ) {

            if ( member.IsInABand() ) {

                RemoveFactionMemberFromBand( member, factionId );

            }

            factionData.RemoveMember( member, optionData.typeOfEntity[0] );

        }

    }

    public bool AddFactionMemberToBand( Character member, int factionId, int bandId ) {

        if( member.IsInAFaction() && member.IsInABand() ) {

            Debug.Log("Ya está en un grupo o no pertenece a la facción.");
            return false;
        
        }

        FactionData factionData = factionsList.GetFactionData( factionId );
        if ( factionData != null ) {

            BandData bandData = factionData.GetBandData( bandId );
            if ( bandData != null ) {

                bandData.AddMember( member, optionData.typeOfEntity[5] );
                return true;

            }
            else {

                Debug.Log( "El grupo no existe." );
                return false;

            }

        }
        else {

            Debug.Log( "La facción no existe." );
            return false;
        
        }

    }

    public bool RemoveFactionMemberFromBand( Character member, int factionId ) {

        if ( !member.IsInABand() ) {

            Debug.Log("No está en ningún grupo.");
            return false;
        
        }

        FactionData factionData = factionsList.GetFactionData( factionId );
        if ( factionData != null ) {

            BandData bandData = factionData.GetBandData( member.GetBandId() );
            if ( bandData != null ) {

                bandData.RemoveMember( member, optionData.typeOfEntity[2] );
                return true;

            }
            else {

                Debug.Log( "No existe el grupo." );
                return false;

            }

        }
        else {

            Debug.Log( "No existe la facción." );
            return false;

        }

    }

}
