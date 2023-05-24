using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AllyHireButton : MonoBehaviour, IPointerClickHandler {

    [SerializeField] Image characterSprite;
    [SerializeField] Text characterName;
    [SerializeField] BasicPanel panel;
    [SerializeField] int allyId;
    [SerializeField] bool isEmpty;

    [SerializeField] private int myIndex;

    private void Awake( ) {
        
        allyId= -1;
        isEmpty = true;
        panel = transform.parent.transform.parent.GetComponent<BasicPanel>();

    }

    public void SetIndex( int index ) {

        myIndex = index;

    }

    public int GetIndex( ) {
    
        return myIndex;
    
    }

    public Sprite GetAllyImage( ) {
    
        return characterSprite.sprite;
    
    }

    public bool IsEmpty( ) {

        return isEmpty; 

    }

    public int GetAllyId( ) {
    
        return allyId;
    
    }

    public void Set( Character character ) {

        characterSprite.sprite = character.GetComponent<SpriteRenderer>().sprite;
        characterSprite.gameObject.SetActive( true );

        characterName.text = character.GetName();
        characterName.gameObject.SetActive( true );

        allyId = character.GetId();

        isEmpty = false;

    }

    public void Clear( ) {

        characterSprite.sprite = null;
        allyId = -1;
        isEmpty = true;

        characterSprite.gameObject.SetActive( false );
        characterName.gameObject.SetActive( false );

    }

    public void OnPointerClick( PointerEventData eventData ) {

        panel.OnClick( myIndex );

    }

}
