using System;
using UnityEngine;

/// <summary>
/// Contenedores de los personajes. Aquí es donde se equipan los objetos.
/// </summary>
public class InventoryContainer {

    InventoryCharacter inventoryCharacter;

    public ItemType itemType;
    public EquipableType equipableType;

    BasicItem[,] items;

    public int width;
    public int height;

    bool itsBeingUsed = false;
    ItemContainer itsBeingUsedBy;

    public delegate void Equip( EquipableType equipType, BasicItem equipedItem );
    public event Equip OnEquip;

    public delegate void Disequip( EquipableType equipType );
    public event Disequip OnDisEquip;


    public InventoryContainer( int equipedItemWidth, int equipedItemHeight, EquipableType equipableType, ItemType itemType, InventoryCharacter inventoryCharacter, bool itsBeingUsed ) {

        this.equipableType = equipableType;
        this.itemType = itemType;
        this.width = equipedItemWidth;
        this.height = equipedItemHeight;
        items = new BasicItem[10, 10];

        this.inventoryCharacter = inventoryCharacter;
        this.itsBeingUsed = itsBeingUsed;
        itsBeingUsedBy = null;
    
    }

    public bool CheckBoundaries( BasicItem item, Vector2Int origin ) {

        if ( item.basicItemData.width + origin.x <=  width &&
             item.basicItemData.height + origin.y <= height ) {

            return true;
        
        }

        return false;
    
    }
    public bool ItsUsed( ) {

        return itsBeingUsed;
    
    }

    public void SetItsUsedBy( ItemContainer itemContainer ) {

        itsBeingUsedBy = itemContainer;
    
    }

    public ItemContainer GetItsUsedBy( ) {

        return itsBeingUsedBy;
    
    }

    public void SetUsed( bool used ) {
    
        itsBeingUsed = used;
    
    }

    public void ChangeSpace( int width, int height ) {

        this.width = width;
        this.height = height;

    }

    public bool CheckIfThereIsItem( int posX, int posY ) {

        return items[posX, posY] != null;

    }

    public bool CheckIfOverlapingItem( BasicItem item, Vector2Int originPos ) {

        for ( int i = 0; i < item.basicItemData.height; i++ ) {

            for ( int j = 0; j < item.basicItemData.width; j++ ) {

                if ( items[originPos.x + j, originPos.y + i] != null ) {

                    return true;

                }

            }

        }

        return false;

    }

    public bool PlaceItem( BasicItem itemToPlace, int posX, int posY ) {

        if ( itemType != ItemType.ArmItem && itemType != ItemType.All ) {

            if ( itemToPlace.basicItemData.equipableType != equipableType ) {

                return false;

            }

        }
         
        for ( int i = 0; i < itemToPlace.basicItemData.height; i++ ) {

            for ( int j = 0; j < itemToPlace.basicItemData.width; j++ ) {

                items[posX + j, posY + i] = itemToPlace;

            }

        }

        OnEquip?.Invoke( equipableType, itemToPlace );

        return true;

    }

    /// <summary>
    /// Searches an item that is overlaped by itemWantToPlace.
    /// </summary>
    /// <param name="itemWantToPlace"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public BasicItem SearchAndPickItem( BasicItem itemWantToPlace, Vector2Int origin ) {

        BasicItem itemToSearchAndPick = SearchItem( itemWantToPlace, origin );

        ClearAllReferencesFromItem( itemToSearchAndPick );

        return itemToSearchAndPick;

    }

    /// <summary>
    /// Searches an item that is overlaped by itemWantToPlace, but it does not delete it.
    /// </summary>
    /// <param name="itemWantToPlace"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public BasicItem SearchItem( BasicItem itemWantToPlace, Vector2Int origin ) {

        if ( itemWantToPlace == null ) {

            return null;
        
        }

        for ( int i = 0; i < itemWantToPlace.basicItemData.height; i++ ) {

            for ( int j = 0; j < itemWantToPlace.basicItemData.width; j++ ) {

                if ( items[origin.x + j, origin.y + i] != null ) {

                    return items[origin.x + j, origin.y + i];

                }

            }

        }

        return null;

    }

    public void ClearAllReferencesFromItem( BasicItem itemToSearchAndPick ) {

        for ( int i = 0; i < items.GetLength( 0 ); i++ ) {

            for ( int j = 0; j < items.GetLength( 1 ); j++ ) {

                if ( items[i, j] == itemToSearchAndPick ) {

                    items[i, j] = null;

                }

            }

        }

        OnDisEquip?.Invoke( equipableType );

    }

    public BasicItem PickItem( int posX, int posY ) {

        BasicItem itemToPick = items[posX, posY];

        ClearAllReferencesFromItem( itemToPick );

        return itemToPick;

    }

    public Vector2Int GetOriginOfAnItem( BasicItem item ) {

        Vector2Int origin = Vector2Int.zero;

        for ( int i = 0; i < height; i++ ) {

            for ( int j = 0; j < width; j++ ) {

                if ( items[j, i] == item ) {

                    origin.x = j - item.basicItemData.width + 1;
                    origin.y = i - item.basicItemData.height + 1;

                }

            }

        }

        return origin;

    }


    /// <summary>
    /// Este no elimina el item del contenedor, solo devuelve la referencia.
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <returns></returns>
    public BasicItem GetItemInPos( int posX, int posY ) {

        return items[posX, posY];

    }

    public BasicItem[,] GetItems( ) {

        return items;

    }

    public void ShowListContainer( ) {

        string list = "";

        for ( int i = 0; i < height; i++ ) {

            list += "\n";

            for ( int j = 0; j < width; j++ ) {

                if ( items[j, i] != null ) {

                    list += " " + items[j, i].basicItemData.nombreItem + " ";

                }
                else {

                    list += " " + "null" + " ";

                }
            
            }
        
        }

        Debug.Log( list );
    
    }

}
