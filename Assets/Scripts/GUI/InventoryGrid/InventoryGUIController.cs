using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUIController : MonoBehaviour {

    InventoryGrid selectedGrid;
    [SerializeField] InventoryGrid[] mainContainers;

    BasicItemObject draggedItemObject;
    BasicItem draggedItem;

    [SerializeField] InventoryItemHighlight highlighter;

    [SerializeField] InventoryController characterInventoryController;

    ObjectPool poolItem;
    ObjectPool poolItemContainer;
    [SerializeField] GameObject prefabItem;
    [SerializeField] GameObject prefabItemContainer;
    [SerializeField] GameObject itemFather;
    [SerializeField] GameObject itemContainerFather;

    [SerializeField] ItemContainerData[] listItemContainerData;
    [SerializeField] ItemData[] listItemData;

    // Control de inputs:
    private bool canUserClick = true;

    private void Awake( ) {

        poolItem = new ObjectPool( prefabItem, 100, itemFather );
        poolItemContainer = new ObjectPool( prefabItemContainer, 100, itemContainerFather );
        
    }

    private void Start( ) {

        gameObject.SetActive( false );

    }

    private void Update( ) {

        if ( draggedItemObject != null ) {

            draggedItemObject.transform.position = Input.mousePosition;

        }

        if ( Input.GetKeyDown( KeyCode.Q ) ) {

            if ( draggedItemObject != null && draggedItemObject is ItemObject ) {

                poolItem.ReturnObject( draggedItemObject.gameObject );
                draggedItemObject = null;
                draggedItem = null; 

            }
            else if ( draggedItemObject != null && draggedItemObject is ItemContainerObject ) {

                poolItemContainer.ReturnObject( draggedItemObject.gameObject );
                draggedItemObject = null;
                draggedItem = null;
            
            }

            CreateInstanceOfBasicObject(); 
            
        }

        Highlight();

        if ( selectedGrid == null ) {

            return;

        }

        if ( Input.GetMouseButtonDown( 0 ) && canUserClick ) {

            canUserClick = false;
            StartCoroutine( ExecuteActionAndWaitForCompletion() );

        }

        if ( Input.GetKeyDown( KeyCode.T ) ) {

            if ( selectedGrid.gridData.gridType == GridType.Normal ) {

                characterInventoryController.inventoryCharacter.GetContainers()[selectedGrid.gridData.equipableType.ToString()].ShowListContainer();

            }
            else if ( selectedGrid.gridData.gridType == GridType.Main ) {

                characterInventoryController.inventoryCharacter.GetMainContainers()[selectedGrid.gridData.equipableType.ToString()].ShowListContainer();

            }
        
        }

    }

    public void PickPlaceOrReplaceItemIcon( Vector2Int posOnGrid ) {

        BasicItem itemToPut = characterInventoryController.itemsToSwap.itemToPut;
        BasicItem itemToDrag = characterInventoryController.itemsToSwap.itemToDrag;

        BasicItem oldDraggedItem = draggedItem;

        RemoveDraggedItem();

        SetItemToDrag( itemToDrag );

        SetItemToPut( itemToPut, oldDraggedItem, posOnGrid );

        SetMainContainer( itemToPut );

    }

    public void SetMainContainer( BasicItem itemToPut ) {

        InventoryContainer mainContainer = characterInventoryController.itemsToSwap.mainContainerToPut;
        InventoryContainer mainContainer2 = characterInventoryController.itemsToSwap.mainContainerToQuit;

        if ( mainContainer2 != null ) {

            foreach ( InventoryGrid grid in mainContainers ) {

                if ( grid.gridData.equipableType == mainContainer2.equipableType ) {

                    grid.gameObject.SetActive( false );

                }

            }

        }

        if ( mainContainer != null ) {

            foreach ( InventoryGrid grid in mainContainers ) {

                if ( grid.gridData.equipableType == mainContainer.equipableType ) {

                    grid.gameObject.SetActive( true );
                    grid.ChangeSpaceFromContainer( (itemToPut as ItemContainer).itemContainerData.spaceWidth, (itemToPut as ItemContainer).itemContainerData.spaceHeight );
                    grid.SetDimensionFromSpace();

                }

            }

        }

        characterInventoryController.itemsToSwap.mainContainerToPut = null;
        characterInventoryController.itemsToSwap.mainContainerToQuit = null;

    }

    public void SetItemToPut( BasicItem itemToPut, BasicItem oldDraggedItem, Vector2Int posOnGrid ) {

        if ( itemToPut != null && itemToPut is Item && itemToPut == oldDraggedItem ) {

            GameObject go = poolItem.GetObject();
            go.transform.SetParent( selectedGrid.transform );
            go.GetComponent<ItemObject>().item = itemToPut as Item;
            go.GetComponent<ItemObject>().SetSpriteIcon();
            PlaceItemIcon( go.GetComponent<BasicItemObject>(), posOnGrid );

        }
        else if ( itemToPut != null && itemToPut is ItemContainer && itemToPut == oldDraggedItem ) {

            GameObject go = poolItemContainer.GetObject();
            go.transform.SetParent( selectedGrid.transform );
            go.GetComponent<ItemContainerObject>().itemContainer = itemToPut as ItemContainer;
            go.GetComponent<ItemContainerObject>().SetSpriteIcon();
            PlaceItemIcon( go.GetComponent<BasicItemObject>(), posOnGrid );

        }

    }

    public void SetItemToDrag( BasicItem itemToDrag ) {

        if ( itemToDrag != null && itemToDrag is Item ) {

            foreach ( ItemObject item in selectedGrid.gameObject.GetComponentsInChildren<ItemObject>() ) {

                if ( (itemToDrag as Item) == item.item ) {

                    item.transform.SetParent( itemFather.transform );
                    poolItem.ReturnObject( item.gameObject );
                    break;

                }

            }

            draggedItemObject = poolItem.GetObject().GetComponent<BasicItemObject>();
            (draggedItemObject as ItemObject).item = (itemToDrag as Item);
            (draggedItemObject as ItemObject).SetSpriteIcon();
            draggedItem = (itemToDrag as Item);

        }
        else if ( itemToDrag != null && itemToDrag is ItemContainer ) {

            foreach ( ItemContainerObject item in selectedGrid.gameObject.GetComponentsInChildren<ItemContainerObject>() ) {

                if ( (itemToDrag as ItemContainer) == item.itemContainer ) {

                    item.transform.SetParent( itemContainerFather.transform );
                    poolItem.ReturnObject( item.gameObject );
                    break;

                }

            }

            draggedItemObject = poolItemContainer.GetObject().GetComponent<BasicItemObject>();
            (draggedItemObject as ItemContainerObject).itemContainer = (itemToDrag as ItemContainer);
            (draggedItemObject as ItemContainerObject).SetSpriteIcon();
            draggedItem = (itemToDrag as ItemContainer);

        }

    }

    public void RemoveDraggedItem( ) {

        if ( draggedItemObject != null && draggedItemObject is ItemObject ) {

            draggedItemObject.transform.SetParent( itemFather.transform );
            poolItem.ReturnObject( draggedItemObject.gameObject );
            draggedItemObject = null;
            draggedItem = null;

        }
        else if ( draggedItemObject != null && draggedItemObject is ItemContainerObject ) {

            draggedItemObject.transform.SetParent( itemContainerFather.transform );
            poolItemContainer.ReturnObject( draggedItemObject.gameObject );
            draggedItemObject = null;
            draggedItem = null;

        }

    }

    public void PlaceItemIcon( BasicItemObject itemToPlace, Vector2Int posOnGrid ) {

        RectTransform rectTransformItem = itemToPlace.GetComponent<RectTransform>();
        rectTransformItem.SetParent( selectedGrid.GetComponent<RectTransform>() );

        Vector2 position = Vector2.zero;
        position.x = (posOnGrid.x * GridData.tileSizeWidth);
        position.y = -(posOnGrid.y * GridData.tileSizeHeight);

        rectTransformItem.localPosition = position;

    }

    public void SelectGrid( InventoryGrid inventoryGrid ) {
    
        selectedGrid = inventoryGrid;
    
    }

    public void CreateInstanceOfBasicObject( ) {

        GameObject item = null;
        BasicItemObject itemObject = null;

        if ( UnityEngine.Random.Range(0, 2) == 0 ) {

            item = poolItem.GetObject();
            itemObject = item.GetComponent<BasicItemObject>();

        }
        else{

            item = poolItemContainer.GetObject();
            itemObject = item.GetComponent<BasicItemObject>();

        }

        draggedItemObject = itemObject;
        draggedItemObject.gameObject.SetActive( true );
        
        if ( itemObject != null ) {

            if ( itemObject is ItemObject ) {

                ItemObject auxItem = itemObject as ItemObject;
                auxItem.item.SetItemData( listItemData[UnityEngine.Random.Range(0, listItemData.GetLength(0))] );
                auxItem.SetSpriteIcon();
                draggedItem = auxItem.item;

            }
            else if ( itemObject is ItemContainerObject ) {

                ItemContainerObject auxItem = itemObject as ItemContainerObject;
                auxItem.itemContainer.SetItemContainerData( listItemContainerData[UnityEngine.Random.Range( 0, listItemContainerData.Length )] );
                auxItem.SetSpriteIcon();
                draggedItem = auxItem.itemContainer;

            }
        
        }
    
    }

    IEnumerator ExecuteActionAndWaitForCompletion( ) {

        Vector2Int posOnGrid = selectedGrid.GetTileGridPositionFromMousePosition( Input.mousePosition );

        if ( characterInventoryController.PickPlaceOrReplaceItem( selectedGrid.gridData.equipableType, draggedItem, posOnGrid, selectedGrid.gridData.gridType ) ) {

            PickPlaceOrReplaceItemIcon( posOnGrid );

        }

        yield return null; 

        canUserClick = true; 
    }

    public void Highlight( ) {

        Vector2Int posOnGrid;

        if ( selectedGrid != null ) {

            posOnGrid = selectedGrid.GetTileGridPositionFromMousePosition( Input.mousePosition );

        }
        else {

            highlighter.ShowHighlighter( false );
            return;

        }

        if ( draggedItem == null ) {

            if ( selectedGrid.gridData.gridType == GridType.Normal ) {

                InventoryContainer container = characterInventoryController.inventoryCharacter.GetContainers()[selectedGrid.gridData.equipableType.ToString()];

                BasicItem item = container.GetItemInPos( posOnGrid.x, posOnGrid.y );

                if ( item != null ) {

                    Vector2 origin = container.GetOriginOfAnItem( item );
                    SetHighlighter( item, origin );

                    return;

                }
                else {

                    highlighter.ShowHighlighter( false );

                }

            }
            else if ( selectedGrid.gridData.gridType == GridType.Main ) {

                InventoryContainer container = characterInventoryController.inventoryCharacter.GetMainContainers()[selectedGrid.gridData.equipableType.ToString()];

                BasicItem item = container.GetItemInPos( posOnGrid.x, posOnGrid.y );

                if ( item != null ) {

                    Vector2 origin = container.GetOriginOfAnItem( item );
                    SetHighlighter( item, origin );

                    return;

                }
                else {

                    highlighter.ShowHighlighter( false );

                }

            }

        }
        else {

            if ( selectedGrid.gridData.gridType == GridType.Normal ) {

                InventoryContainer container = characterInventoryController.inventoryCharacter.GetContainers()[selectedGrid.gridData.equipableType.ToString()];

                if ( container.CheckBoundaries( draggedItem, posOnGrid ) ) {

                    SetHighlighter( draggedItem, (Vector2) posOnGrid );

                }
                else {

                    highlighter.ShowHighlighter( false );

                }

            }
            else if ( selectedGrid.gridData.gridType == GridType.Main ) {

                InventoryContainer container = characterInventoryController.inventoryCharacter.GetMainContainers()[selectedGrid.gridData.equipableType.ToString()];

                if ( container.CheckBoundaries( draggedItem, posOnGrid ) ) {

                    SetHighlighter( draggedItem, (Vector2) posOnGrid );

                }
                else {

                    highlighter.ShowHighlighter( false );

                }

            }

        }

    }

    public void SetHighlighter( BasicItem item, Vector2 pos ) {

        highlighter.highlighter.SetParent( selectedGrid.transform );
        highlighter.highlighter.SetAsFirstSibling();

        highlighter.SetSizeHighlighter( item );
        highlighter.SetPosHighlighter( pos, selectedGrid.transform.position );
        highlighter.ShowHighlighter( true );

    }

    public void ShowInventory( ) {
    
        gameObject.SetActive( !gameObject.activeInHierarchy );
    
    }

}
