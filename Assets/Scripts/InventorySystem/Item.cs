using UnityEngine;

/// <summary>
/// Holds the information about a specific item in the world.
/// </summary>
public class Item : BasicItem {

    [SerializeField] public ItemData itemData;

    public Item( ) {
   
    
    }

    public void SetItemData( ItemData itemData ) {

        this.itemData = itemData;
        basicItemData = itemData;

    }

}
