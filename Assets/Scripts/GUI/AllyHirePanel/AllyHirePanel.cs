using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Deber�a llamarse AllyBandPanel y viceversa.
/// </summary>
public class AllyHirePanel : BasicPanel {

    [SerializeField] private List<AllyHireButton> allyHiredButtons;

    AllyHireButton button;
    Vector2 localPos = new Vector2( 1, 0 );

    [SerializeField] protected int draggingAllyId;
    [SerializeField] protected Character draggingAlly;

    [SerializeField] protected int maxAllyHire;

    [SerializeField] protected GameObject draggedAlly; // Es el objeto del editor.

    RectTransform iconTransform;
    protected Image draggedItemIconImage;

    private void Start( ) {

        Init();
        playerBand = GameManager.instance.factionsController.GetBandData( playerCharacter.GetFactionId(), playerCharacter.GetBandId() );

        maxAllyHire = allyHiredButtons.Count; // No pueden haber m�s aliados que botones de aliados.

        iconTransform = draggedAlly.GetComponent<RectTransform>();
        draggedItemIconImage = draggedAlly.GetComponent<Image>();

        draggedItemIconImage.sprite = null;
        draggingAllyId = -1;

        gameObject.SetActive( false );

    }

    private void Update( ) {

        if ( draggedAlly.activeInHierarchy == true ) {

            iconTransform.position = Input.mousePosition;
        }

    }

    protected override void Init( ) {

        SetAlliesButtonsListsIndex();

    }

    /// <summary>
    /// Declara los �ndices de las listas de los botones.
    /// </summary>
    private void SetAlliesButtonsListsIndex( ) {

        for ( int i = 0; i < allyHiredButtons.Count; i++ ) {

            allyHiredButtons[i].SetIndex( i + 1 );
            allyHiredButtons[i].Clear();

        }

    }

    public bool AddMember( Character member ) {

        BandMemberData memberData = playerBand.GetBandMemberDataFromMember( member );

        for ( int i = 0; i < allyHiredButtons.Count; i++ ) {

            if ( allyHiredButtons[i].IsEmpty() && memberData.GetMemberPos() == allyHiredButtons[i].GetIndex() ) {

                allyHiredButtons[i].Set( member ); 

                return true;

            }

        }
        return false;
        
    }

    public bool RemoveMember( Character member ) {

        for ( int i = 0; i < allyHiredButtons.Count; i++ ) {

            if ( allyHiredButtons[i].GetAllyId() == member.GetId() ) {

                allyHiredButtons[i].Clear();
                return true;

            }

        }
        return false;

    }

    public AllyHireButton GetButton( int buttonIndex ) {

        for ( int i = 0; i < allyHiredButtons.Count; i++ ) {

            if ( allyHiredButtons[i].GetIndex() == buttonIndex ) {

                return allyHiredButtons[i];

            }

        }
        return null;

    }

    public override void OnClick( int buttonIndex ) {

        button = GetButton( buttonIndex );

        if ( draggingAllyId == -1 ) { // No hay nada arrastr�ndose.

            if ( !button.IsEmpty() ) { // El bot�n no est� vac�o, por lo tanto, se obtiene el sprite del aliado que est� en el bot�n.
                Debug.Log( button.GetAllyId() );

                UpdateIconFromButton( playerBand.GetMember( button.GetAllyId() ) );
                button.Clear();
                draggedAlly.SetActive( true );

            }

        }
        else { // Un aliado est� siendo arrastrado.

            if ( button.IsEmpty() ) { // No hay aliado en el bot�n.

                UpdateButtonFromDragged( buttonIndex );
                draggedAlly.SetActive( false );

            }
            else {

                Character buttonMember = playerBand.GetMember( button.GetAllyId() );
                UpdateButtonFromDragged( buttonIndex );
                UpdateIconFromButton( buttonMember );

            }

        }

    }

    public void UpdateIconFromButton( Character member ) {

        draggingAllyId = member.GetId();
        draggingAlly = member;
        draggedItemIconImage.sprite = member.GetComponent<SpriteRenderer>().sprite;
        member.GetComponent<CharacterControllerAI>().SetLocalPosToWaitOrders( localPos );

    }

    public void UpdateButtonFromDragged( int buttonIndex ) {

        playerBand.GetBandMemberDataFromMember( playerBand.GetMember( button.GetAllyId() ) ).ClearMember(); // Aqu� se elimina del bandMemberData en el que estaba.

        button.Set( draggingAlly );
        playerBand.UpdatePos( draggingAlly, buttonIndex );

        draggingAllyId = -1;
        draggingAlly = null;

    }

}
