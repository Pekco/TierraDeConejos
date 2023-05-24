using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCOptionMenuButton : MonoBehaviour, IPointerClickHandler {

    [SerializeField] Text optionName;
    [SerializeField] int option;
    [SerializeField] int myIndex;

    public void SetIndex( int index ) {

        myIndex = index;

    }

    public void SetOptionName( string optionName ) {

        this.optionName.text = optionName;

    }

    public void SetOption( int option ) {

        this.option = option;

    }

    public void ShowButton( ) {
    
        gameObject.SetActive( true );
    
    }

    public void HideButton( ) {
    
        gameObject.SetActive( false );
    
    }

    public void Clean( ) {

        optionName.gameObject.SetActive( false );

    }

    public void OnPointerClick( PointerEventData eventData ) {

        NPCMenu npcMenu = transform.GetComponentInParent<NPCMenu>();
        npcMenu.OnClick( option );

    }

}
