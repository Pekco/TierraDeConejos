using UnityEngine;
using static InventoryContainer;

/// <summary>
/// Holds information about a specific container.
/// </summary>
public class ItemContainer : BasicItem {

    BasicItem[,] items;

    public ItemContainerData itemContainerData;

    public ItemContainer() {

    }

    public void SetItemContainerData( ItemContainerData itemContainerData ) {

        this.itemContainerData = itemContainerData;
        basicItemData = itemContainerData;

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

    public void PlaceItem( BasicItem itemToPlace, int posX, int posY ) {

        for ( int i = 0; i < itemToPlace.basicItemData.height; i++ ) {

            for ( int j = 0; j < itemToPlace.basicItemData.width; j++ ) {

                items[posX + j, posY + i] = itemToPlace;

            }

        }

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
    }

    public BasicItem PickItem( int posX, int posY ) {

        BasicItem itemToPick = items[posX, posY];

        ClearAllReferencesFromItem( itemToPick );

        return itemToPick;

    }

    public Vector2Int GetOriginOfAnItem( BasicItem item ) {

        Vector2Int origin = Vector2Int.zero;

        for ( int i = 0; i < items.GetLength( 0 ); i++ ) {

            for ( int j = 0; j < items.GetLength( 1 ); j++ ) {

                if ( items[i, j] == item ) {

                    origin.x = i - item.basicItemData.width + 1;
                    origin.y = j - item.basicItemData.height + 1;

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

    public void ShowItemList( ) {

        
        string list = "";

        for ( int i = 0; i < items.GetLength(0); i++ ) {

            list += "\n";

            for ( int j = 0; j < items.GetLength(1); j++ ) {

                if ( items[j, i] != null ) {

                    list += " ";
                    list = list + items[j, i].nombreItem + " ";

                }
                else {

                    list += " Vacío ";

                }
            
            }
        
        }

        UnityEngine.Debug.Log(list);

    }

}
