using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController : MonoBehaviour {

    InventoryController inventoryController;
    InventoryCharacter inventoryCharacter;

    [SerializeField] EquipContainer[] equipContainers;

    private void Awake( ) {
        
        inventoryController = GetComponentInParent<InventoryController>();

    }

    private void Start( ) {

        inventoryCharacter = inventoryController.inventoryCharacter;

        foreach ( InventoryContainer container in inventoryCharacter.GetContainers().Values ) {

            container.OnEquip += EquipItem;
            container.OnDisEquip += DisequipItem;
        
        }

    }

    private void OnEnable( ) {

        if ( inventoryCharacter != null ) {

            foreach ( InventoryContainer container in inventoryCharacter.GetContainers().Values ) {

                container.OnEquip += EquipItem;
                container.OnDisEquip += DisequipItem;

            }

        }

    }

    private void OnDisable( ) {

        if ( inventoryCharacter != null ) {

            foreach ( InventoryContainer container in inventoryCharacter.GetContainers().Values ) {

                container.OnEquip -= EquipItem;
                container.OnDisEquip += DisequipItem;

            }

        }

    }

    public void EquipItem( EquipableType equipableType, BasicItem itemToEquip ) {

        foreach ( EquipContainer equipContainer in equipContainers ) {

            if ( equipContainer.equipableType == equipableType ) {

                equipContainer.ChangeEquip( itemToEquip );
            
            }
        
        }
    
    }

    public void DisequipItem( EquipableType equipableType ) {

        foreach ( EquipContainer equipContainer in equipContainers ) {

            if ( equipContainer.equipableType == equipableType ) {

                equipContainer.Disequip( );

            }

        }

    }

}
