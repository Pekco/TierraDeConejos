using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMenu : MonoBehaviour {

    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Vector3 characterPos;

    [SerializeField] Character player;
    [SerializeField] Character npcCharacter;
    [SerializeField] Text npcCharacterName;

    [SerializeField] Camera cam;
    [SerializeField] NPCMenuOptionDataContainer options;
    [SerializeField] FactionsController factionsController;
    [SerializeField] AllyHirePanel allyHirePanel;

    [SerializeField] private List<NPCOptionMenuButton> npcMenuButtons;

    [SerializeField] private float distanceBetweenPlayerCharacter = 0;
    [SerializeField] private float maxDistanceMenuOpen = 0;

    private void Awake( ) {

        maxDistanceMenuOpen = 30.0f;
        Init();
        npcCharacterName = gameObject.transform.Find( "EntityName" ).Find( "NameText" ).gameObject.GetComponent<Text>();

        factionsController = GameManager.instance.factionsController;
        allyHirePanel = GameManager.instance.hirePanelAlly;

    }

    private void Update( ) {

        UpdateMenuPosition();

        if (distanceBetweenPlayerCharacter > maxDistanceMenuOpen ) {

            CloseMenu();
        
        }

    }

    private void FixedUpdate( ) {

        DistanceBetweenPlayerCharacter();

    }

    public void Init( ) {

        options.Init( );
        SetNpcMenuButtonsListIndex();

    }

    public void OpenNPCMenu ( Character npcCharacter, Character player, float sizeInteractableArea ) {

        this.player = player;
        this.playerPos = player.transform.position;
        
        this.npcCharacter = npcCharacter;
        this.characterPos = npcCharacter.transform.position;

        DistanceBetweenPlayerCharacter();

        npcCharacterName.text = npcCharacter.GetName();
        //maxDistanceMenuOpen = sizeInteractableArea;

        
        UpdateNpcMenuButtons();
        UpdateMenuPosition();
        Show( true );

    }

    public void Show( bool show ) {

        gameObject.SetActive( show );

    }

    /// <summary>
    /// Declara los índices de las listas de los botones.
    /// </summary>
    private void SetNpcMenuButtonsListIndex( ) {

        for ( int i = 0; i < npcMenuButtons.Count; i++ ) {

            npcMenuButtons[i].SetIndex( i );

        }

    }

    /// <summary>
    /// Actualiza las listas de los botones dependiendo del tipo de npc.
    /// ATENCIÓN: En el editor hay un máximo de 6 botones para las opciones.
    /// </summary>
    public void UpdateNpcMenuButtons( ) {

        if ( options.menus[npcCharacter.tag].Length <= 6 ) {

            int i = 0;
            foreach ( string option in options.menus[npcCharacter.tag] ) {

                npcMenuButtons[i].SetOptionName(option);
                npcMenuButtons[i].SetOption( options.optionsAux[option] );
                npcMenuButtons[i].ShowButton();
                i++;

            }

            HideUnusedButtons(i); // Esconde los botones restantes en el momento que deja de haber opciones.

        }

    }

    /// <summary>
    /// Ejecuta la acción de la opción.
    /// </summary>
    public void OnClick( int option) {

        Show( false );
        switch ( option ) {

            case 1:

                factionsController.AddMemberToFaction( npcCharacter, player.GetFactionId() );

                break;

            case 2:

                factionsController.RemoveMemberFromFaction( npcCharacter, player.GetFactionId() );

                break;

            case 3:

                if ( factionsController.AddFactionMemberToBand( npcCharacter, player.GetFactionId(), player.GetBandId() ) ) {

                    allyHirePanel.AddMember( npcCharacter );

                }

                break;

            case 4:

                if ( factionsController.RemoveFactionMemberFromBand( npcCharacter, player.GetFactionId() ) ) {

                    allyHirePanel.RemoveMember( npcCharacter );

                }

                break;

        }

        UpdateNpcMenuButtons();
        Show(true);

    }

    public void DistanceBetweenPlayerCharacter( ) {

        playerPos = player.transform.position;
        distanceBetweenPlayerCharacter = Vector3.Distance(playerPos, npcCharacter.transform.position);
    
    }

    public void CloseMenu( ) {

        Show( false );
        npcCharacter = null;
        playerPos = Vector3.zero;
    
    }

    public void UpdateMenuPosition( ) {

        transform.position = cam.WorldToScreenPoint( npcCharacter.transform.position );// La posición del menú es la misma que la del npc.

    }

    public void HideUnusedButtons( int indexButton) {
    
        for(int i = indexButton; i < npcMenuButtons.Count; i++ ) {

            npcMenuButtons[i].HideButton();
        
        }
    
    }

}
