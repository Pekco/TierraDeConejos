using UnityEngine;

public class StatusPanel : MonoBehaviour {

    private void Awake( ) {
        
        gameObject.SetActive( false );

    }

    public void ShowStatusPanel( bool show ) {
    
        gameObject.SetActive( show );
    
    }

}
