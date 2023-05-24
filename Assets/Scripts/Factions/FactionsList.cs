using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionsList {

    [SerializeField] private List<FactionData> factions;
    [SerializeField] private int maxFactions;

    public  FactionsList( ) {

        factions = new List<FactionData>();
        maxFactions = 10;

    }

    public int GetFactionLength( ) {
    
        return factions.Count;
    
    }


    public void AddFaction( FactionData faction ) {

        if ( factions.Count < maxFactions ) {

            if ( factions != null && !factions.Contains( faction ) ) {

                factions.Add( faction );
                faction.AddNewId();

            }
            else {

                Debug.Log( "Esta facción ya está creada." );

            }

        }
        else {

            Debug.Log( "Máximo de facciones alcanzada." );
        
        }

    }

    public void RemoveFaction( FactionData faction ) {

        if ( factions != null && factions.Contains( faction ) ) {

            factions.Remove( faction );

        }
        else {

            Debug.Log( "El tipo este no está en la facción." );

        }

    }

    public FactionData GetFactionData( int id ) {

        for(int i = 0; i < factions.Count; i++ ) {

            if ( factions[i].GetFactionId() == id ) {

                return factions[i];
            
            }
        
        }

        return null;
    
    }


    /// <summary>
    /// Fills the factions list with empty information, just for optimization.
    /// </summary>
    public void InitializeFactionsAndBands( ) {
    
        for(int i = 0; i < maxFactions; i++) {

            factions.Add( new FactionData() );
            factions[i].SetFactionId(i);
        
        }

    }

}
