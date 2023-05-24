using UnityEngine;


/// <summary>
/// Declara los índices de los botones de aliados  y los actualiza.
/// </summary>
public class BasicPanel : MonoBehaviour {

    [SerializeField] protected Character playerCharacter;

    protected BandData playerBand;

    protected virtual void Init( ) {


    }


    public void ShowPanelAllies( bool show ) {

        gameObject.SetActive( show );

    }


    public virtual void OnClick( int buttonIndex ) {



    }

    public virtual void OnClick( int buttonIndex, bool activeClick ) {



    }

}
