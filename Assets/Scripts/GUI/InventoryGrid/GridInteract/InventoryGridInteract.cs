using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryGrid))]
/// <summary>
/// This class is for interacting with mouse position and click handler.
/// </summary>
public class InventoryGridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private InventoryGUIController inventoryGUIController;

    InventoryGrid inventoryGrid;

    private void Awake( ) {
        
        inventoryGrid = GetComponent<InventoryGrid>();

    }

    public virtual void OnPointerEnter( PointerEventData eventData ) {

        inventoryGUIController.SelectGrid( inventoryGrid ); 
    
    }

    public virtual void OnPointerExit( PointerEventData eventData ) {

        inventoryGUIController.SelectGrid( null );

    }
}
