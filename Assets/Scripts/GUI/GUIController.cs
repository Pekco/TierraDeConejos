using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GUIController : MonoBehaviour {

    PlayerController characterController;
    Rigidbody2D rb2d;
    [SerializeField] Character character;

    [SerializeField] float offsetDistance = 1.0f; 
    [SerializeField] float sizeInteractableArea;

    [SerializeField] HighlightController highlightController;
    [SerializeField] NPCMenu npcMenu;
    [SerializeField] AllyHirePanel alliesPanel;
    [SerializeField] InventoryGUIController inventoryGUIController;
    [SerializeField] StatusPanel statusPanel;

    private void Awake( ) {

        characterController = GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        sizeInteractableArea = 60;


    }

    private void Start( ) {
        
        npcMenu.gameObject.SetActive( true );
        npcMenu.gameObject.SetActive( false );

    }

    private void Update( ) {

        check();

        if ( Input.GetKeyDown( KeyCode.E ) ) {

            OpenCloseInteractables();

        }
        if ( Input.GetKeyDown( KeyCode.P ) ) {

            alliesPanel.ShowPanelAllies( !alliesPanel.isActiveAndEnabled );
            
        }
        if ( Input.GetKeyDown( KeyCode.Tab ) ) {

            inventoryGUIController.ShowInventory();
        
        }
        if ( Input.GetKeyDown( KeyCode.O ) ) {

            statusPanel.ShowStatusPanel( !statusPanel.isActiveAndEnabled );

        }

    }

    private void check( ) {

        // position crea un area donde recoge los objetos que tengan un collider dentro.
        Vector2 position = rb2d.position;// + characterController.lastMotionVector * offsetDistance;

        // Se guarda un array con todos los objetos con colliders que hay en el area anteriormente creada.
        Collider2D[] colliders = Physics2D.OverlapCircleAll( position, sizeInteractableArea );

        // Busca cualquier objeto con collider.
        foreach ( Collider2D collider in colliders ) {

            Interactable interact = collider.GetComponent<Interactable>();
            if ( interact != null ) {

                highlightController.Highlight( interact.gameObject );
                return;

            }

        }

        highlightController.Hide();

    }

    private void Interact( ) {

        Vector2 position = rb2d.position;// + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll( position, sizeInteractableArea );
        
        foreach ( Collider2D collider in colliders ) {

            if ( collider.GetComponent<Interactable>() != null ) {

                Interactable interact = collider.GetComponent<Interactable>();
                if ( interact != null ) {
                    
                    interact.Interact( character, sizeInteractableArea );
                    break;

                }

            }

        }

    }

    private void OpenCloseInteractables( ) {

        if ( npcMenu.isActiveAndEnabled ) {

            npcMenu.Show( false );

        }
        else {

            Interact();

        }

    }

}
