using System;
using TMPro;
using UnityEngine;


/// <summary>
/// Controlls the position of the band allies depending on the band mode.
/// </summary>
public class PlayerBandModeController : MonoBehaviour {

    [SerializeField] BandModes bandModes;
    [SerializeField] BandData playerBand;

    private void Awake( ) {

        bandModes.Init();

        var dropDownOption = transform.GetComponent<TMP_Dropdown>();
        var bandModesL = Enum.GetNames( typeof( bandModes ) );

        dropDownOption.options.Clear();

        foreach ( string mode in bandModesL ) {

            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData() { text = mode };
            dropDownOption.options.Add( optionData );

        }

        dropDownOption.onValueChanged.AddListener( delegate {
            ChangeBandMode( dropDownOption ); } );

    }

    private void Start( ) {

        playerBand = GameManager.instance.factionsController.GetBandData( 0, 0 );

    }

    private void ChangeBandMode( TMP_Dropdown dropDownOption ) {

        int bandModeValue = dropDownOption.value;

        playerBand.ChangeBandMode( bandModeValue );
    
    }

}
