using UnityEngine;

public class InventoryGrid : MonoBehaviour {

    [SerializeField] public GridData gridData;

    private int width;
    private int height;

    BasicItemObject[,] gridItems;

    RectTransform rectTransformGrid;

    Vector2 currentPositionGrid = Vector2.zero;
    Vector2Int currentTileGridPosition = Vector2Int.zero;

    private void Awake( ) {

        rectTransformGrid = GetComponent<RectTransform>();

        width = gridData.width;
        height = gridData.height;

        SetDimensionFromSpace();

    }

    private void Start() {

        Init();
    
    }

    private void Init( ) {

        

    }

    public void ChangeSpaceFromContainer( int width, int height ) {

        this.width = width;
        this.height = height;

    }

    public void SetDimensionFromSpace( ) {

        Vector2 sizeOfGrid = new Vector2( width * GridData.tileSizeWidth, height * GridData.tileSizeHeight );
        rectTransformGrid.sizeDelta = sizeOfGrid;

    }

    public Vector2Int GetTileGridPositionFromMousePosition( Vector2 mousePosition ) {

        currentPositionGrid.x = mousePosition.x - rectTransformGrid.position.x;
        currentPositionGrid.y = rectTransformGrid.position.y - mousePosition.y;

        currentTileGridPosition.x = ( int ) ( currentPositionGrid.x / GridData.tileSizeWidth );
        currentTileGridPosition.y = ( int ) ( currentPositionGrid.y / GridData.tileSizeHeight );

        return currentTileGridPosition;
    
    }
    /*
    public bool CheckBoundaries( BasicItem item, Vector2Int origin ) {

        if ( item.basicItemData.width + origin.x <= width &&
             item.basicItemData.height + origin.y <= height ) {

            return true;

        }

        return false;

    }

    public void ChangeSpace( int width, int height ) {

        this.width = width;
        this.height = height;

    }

    public bool CheckIfThereIsItem( int posX, int posY ) {

        return gridItems[posX, posY] != null;

    }

    public bool CheckIfOverlapingItem( BasicItemObject item, Vector2Int originPos ) {

        for ( int i = 0; i < item.basicItemData.height; i++ ) {

            for ( int j = 0; j < item.basicItemData.width; j++ ) {

                if ( gridItems[originPos.x + j, originPos.y + i] != null ) {

                    return true;

                }

            }

        }

        return false;

    }

    public bool PlaceItem( BasicItemObject itemToPlace, int posX, int posY ) {

        if ( gridData.itemType != ItemType.All ) {

            if ( itemToPlace.basicItemData.equipableType != gridData.equipableType ) {

                return false;

            }

        }

        for ( int i = 0; i < itemToPlace.basicItemData.height; i++ ) {

            for ( int j = 0; j < itemToPlace.basicItemData.width; j++ ) {

                gridItems[posX + j, posY + i] = itemToPlace;

            }

        }

        return true;

    }

    /// <summary>
    /// Searches an item that is overlaped by itemWantToPlace.
    /// </summary>
    /// <param name="itemWantToPlace"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public BasicItemObject SearchAndPickItem( BasicItemObject itemWantToPlace, Vector2Int origin ) {

        BasicItemObject itemToSearchAndPick = SearchItem( itemWantToPlace, origin );

        ClearAllReferencesFromItem( itemToSearchAndPick );

        return itemToSearchAndPick;

    }

    /// <summary>
    /// Searches an item that is overlaped by itemWantToPlace, but it does not delete it.
    /// </summary>
    /// <param name="itemWantToPlace"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public BasicItemObject SearchItem( BasicItemObject itemWantToPlace, Vector2Int origin ) {

        if ( itemWantToPlace == null ) {

            return null;

        }

        for ( int i = 0; i < itemWantToPlace.basicItemData.height; i++ ) {

            for ( int j = 0; j < itemWantToPlace.basicItemData.width; j++ ) {

                if ( gridItems[origin.x + j, origin.y + i] != null ) {

                    return gridItems[origin.x + j, origin.y + i];

                }

            }

        }

        return null;

    }

    public void ClearAllReferencesFromItem( BasicItemObject itemToSearchAndPick ) {

        for ( int i = 0; i < gridItems.GetLength( 0 ); i++ ) {

            for ( int j = 0; j < gridItems.GetLength( 1 ); j++ ) {

                if ( gridItems[i, j] == itemToSearchAndPick ) {

                    gridItems[i, j] = null;

                }

            }

        }
    }

    public BasicItemObject PickItem( int posX, int posY ) {

        BasicItemObject itemToPick = gridItems[posX, posY];

        ClearAllReferencesFromItem( itemToPick );

        return itemToPick;

    }

    public Vector2Int GetOriginOfAnItem( BasicItemObject item ) {

        Vector2Int origin = Vector2Int.zero;

        for ( int i = 0; i < gridItems.GetLength( 0 ); i++ ) {

            for ( int j = 0; j < gridItems.GetLength( 1 ); j++ ) {

                if ( gridItems[i, j] == item ) {

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
    public BasicItemObject GetItemInPos( int posX, int posY ) {

        return gridItems[posX, posY];

    }

    public BasicItemObject[,] GetItems( ) {

        return gridItems;

    }

    public void ShowListContainer( ) {

        string list = "";

        for ( int i = 0; i < height; i++ ) {

            list += "\n";

            for ( int j = 0; j < width; j++ ) {

                if ( gridItems[j, i] != null ) {

                    list += " " + gridItems[j, i].basicItemData.nombreItem + " ";

                }
                else {

                    list += " " + "null" + " ";

                }

            }

        }

        Debug.Log( list );

    }
    */

}
