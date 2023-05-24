using System.Collections.Generic;
using UnityEngine;

public class BandLeaderOrders : ScriptableObject {

    private Dictionary<int, string> Orders;

    private string[] nombresOrdenes = new string[] {

        "Stop",
        "Move"
    
    };

    public void Init( ) {

        Orders = new Dictionary<int, string>();

        for ( int i = 0; i < nombresOrdenes.Length; i++ ) {

            Orders.Add(i + 1, nombresOrdenes[i] );
        
        }
    
    }

    public string GetOrderNameByIndex( int index ) {

        return Orders.GetValueOrDefault( index );
    
    }

    public void Order( int indexOrder, BandData band ) {

        switch ( indexOrder ) {

            case 1:

                StopAllies( band );

                break;
        
        }
    
    }

    private void StopAllies( BandData band ) {

        band.StandStillAllies( true );
    
    }

    private void MoveAllies( BandData band ) {

        band.StandStillAllies( false );
    
    }

}
