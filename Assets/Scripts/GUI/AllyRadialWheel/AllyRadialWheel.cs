using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the behaviour of the allies based on the radial wheel option clicked.
/// </summary>
public class AllyRadialWheel : BasicPanel {

    [SerializeField] protected List<AllyRadialWheelButton> allyRadialWheelButtons;
    [SerializeField] protected BandLeaderOrders bandOrders;

    private void Start( ) {

        Init();
        playerBand = GameManager.instance.factionsController.GetBandData( playerCharacter.GetFactionId(), playerCharacter.GetBandId() );

    }

    protected override void Init( ) {

        SetAlliesRadialWheelButtons();
    
    }

    private void SetAlliesRadialWheelButtons( ) {

        for ( int i = 0; i < allyRadialWheelButtons.Count; i++ ) {

            allyRadialWheelButtons[i].SetIndex( i + 1 );
            allyRadialWheelButtons[i].SetActive( false );
        
        }
    
    }

    public override void OnClick( int buttonIndex, bool activeClick ) {

        playerBand.Order( buttonIndex, activeClick );

    }

}
