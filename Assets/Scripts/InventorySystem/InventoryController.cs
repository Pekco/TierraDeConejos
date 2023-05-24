using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    public InventoryCharacter inventoryCharacter;
    public OldAndNewItem itemsToSwap;

    private void Awake( ) {
        
        inventoryCharacter = new InventoryCharacter();
        itemsToSwap = new OldAndNewItem();

    }

    public bool PickPlaceOrReplaceItem( EquipableType equipableType, BasicItem item, Vector2Int posOnGrid, GridType gridType ) {

        Dictionary<string, InventoryContainer> characterContainer;
        InventoryContainer inventoryContainer = null;

        if ( gridType == GridType.Main ) {

            characterContainer = inventoryCharacter.GetMainContainers();
            inventoryContainer = characterContainer[equipableType.ToString()];

        }
        else {

            characterContainer = inventoryCharacter.GetContainers();
            inventoryContainer = characterContainer[equipableType.ToString()];

        }

        if ( item != null ) {

            if ( !inventoryContainer.CheckBoundaries( item, posOnGrid ) ) {

                itemsToSwap.Set( null, item ); 
                return false;

            }

        }

        // Lo primero es pillar el item que ya está puesto en el inventario, si hay uno.
        BasicItem itemAlreadyInInv = inventoryContainer.GetItemInPos( posOnGrid.x, posOnGrid.y );

        if ( itemAlreadyInInv == null ) {

            itemAlreadyInInv = inventoryContainer.SearchItem( item, posOnGrid );

        }

        if ( itemAlreadyInInv != null ) {

            Vector2Int itemAlreadyInInvOrigin = inventoryContainer.GetOriginOfAnItem( itemAlreadyInInv );
            inventoryContainer.ClearAllReferencesFromItem( itemAlreadyInInv );

            // Podría haber otro, en tal caso, dejamos los dos que ya estaban y devolvemos false.
            BasicItem itemAlreadyInInv2 = inventoryContainer.SearchItem( item, posOnGrid );

            if ( itemAlreadyInInv2 != null ) {

                Vector2Int itemAlreadyInInv2Origin = inventoryContainer.GetOriginOfAnItem( itemAlreadyInInv2 );
                inventoryContainer.ClearAllReferencesFromItem( itemAlreadyInInv2 );

                PlaceItem( inventoryContainer, itemAlreadyInInv2, itemAlreadyInInv2Origin );
                PlaceItem( inventoryContainer, itemAlreadyInInv, itemAlreadyInInvOrigin );
                itemsToSwap.Set( null, item );
                return false;

            }
            else {

                if ( itemAlreadyInInv is ItemContainer ) {

                    itemsToSwap.mainContainerToQuit = inventoryCharacter.RemoveMainContainerFromItemContainer( itemAlreadyInInv as ItemContainer );
                
                }

                return PickPlaceOrReplaceItem_Next( inventoryContainer, item, itemAlreadyInInv, posOnGrid );

            }

        }

        return PickPlaceOrReplaceItem_Next( inventoryContainer, item, itemAlreadyInInv, posOnGrid );

    }

    public bool PickPlaceOrReplaceItem_Next( InventoryContainer container, BasicItem item, BasicItem itemAlreadyInInv, Vector2Int posOnGrid ) {

        if ( item == null ) {

            itemsToSwap.Set( null, itemAlreadyInInv);
            return true;
        
        }
        else if ( PickPlaceOrReplaceItem_Next_Next( container, item, posOnGrid ) ) {

            itemsToSwap.Set( item, itemAlreadyInInv );
            return true;

        }

        return false;

    }

    /// <summary>
    /// Coloca el item dependiendo de si es de la clase item o itemContainer.
    /// </summary>
    /// <param name="container"></param>
    /// <param name="item"></param>
    /// <param name="posOnGrid"></param>
    /// <returns></returns>
    public bool PickPlaceOrReplaceItem_Next_Next( InventoryContainer container, BasicItem item, Vector2Int posOnGrid ) {

        if ( item is Item ) {

            if ( PlaceItem( container, item, posOnGrid ) ) {

                return true;
            
            }

            return false;

        }
        else if ( item is ItemContainer) {

            if ( container.equipableType == EquipableType.Espalda ||
                container.equipableType == EquipableType.Cinturón ||
                container.itemType == ItemType.All ||
                container.itemType == ItemType.ArmItem ) {

                if ( PlaceItem( container, item, posOnGrid ) ) {

                    if ( container.itemType != ItemType.All ) {

                        itemsToSwap.mainContainerToPut = inventoryCharacter.SetMainContainerFromItemContainer( item as ItemContainer );

                    }

                    return true;

                }

            }
            Debug.Log("nope");
            return false;
        
        }

        return false;
    
    }

    public bool PlaceItemInItemContainer( InventoryContainer container, BasicItem item, Vector2Int posOnGrid ) {

        if ( container.GetItemInPos( posOnGrid.x, posOnGrid.y ) is ItemContainer ) {
        
            ItemContainer itemContainer = container.GetItemInPos( posOnGrid.x, posOnGrid.y ) as ItemContainer;

            itemContainer.PlaceItem( item, posOnGrid.x, posOnGrid.y );

            return true;

        }

        return false;

    }

    public BasicItem PickItemInItemContainer( string container, Vector2Int posOnGrid ) {

        Dictionary<string, InventoryContainer> characterContainer = inventoryCharacter.GetContainers();
        InventoryContainer inventoryContainer = characterContainer[container];

        if ( inventoryContainer.GetItemInPos( posOnGrid.x, posOnGrid.y ) is ItemContainer ) {

            ItemContainer itemContainer = inventoryContainer.GetItemInPos( posOnGrid.x, posOnGrid.y ) as ItemContainer;

            return itemContainer.PickItem( posOnGrid.x, posOnGrid.y );

        }

        return null;

    }

    public bool PlaceItem( InventoryContainer container, BasicItem item, Vector2Int posOnGrid ) {

        return container.PlaceItem( item, posOnGrid.x, posOnGrid.y );

    }

    public BasicItem PickItem( string container, Vector2Int posOnGrid ) {

        Dictionary<string, InventoryContainer> characterContainer = inventoryCharacter.GetContainers();
        InventoryContainer inventoryContainer = characterContainer[container];

        return inventoryContainer.PickItem( posOnGrid.x, posOnGrid.y );

    }

}

public class OldAndNewItem {

    public BasicItem itemToPut;
    public BasicItem itemToDrag;
    public InventoryContainer mainContainerToPut;
    public InventoryContainer mainContainerToQuit;

    public OldAndNewItem( ) {

        itemToPut = null;
        itemToDrag = null;

    }

    public void Set( BasicItem itemToPut, BasicItem itemToDrag ) {
    
        this.itemToPut = itemToPut;
        this.itemToDrag = itemToDrag;
    
    }

    public void SetItemToPut( BasicItem itemToPut ) {

        this.itemToPut = itemToPut;
    
    }

    public void SetItemToDrag( BasicItem itemToDrag ) {

        this.itemToDrag = itemToDrag;
    
    }

}

