using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMenuInteract : Interactable {

    [SerializeField] NPCMenu npcMenu;

    public override void Interact( Character playerCharacter, float sizeInteractableArea ) {

        npcMenu.OpenNPCMenu( GetComponent<Character>(), playerCharacter, sizeInteractableArea );

    }

}
