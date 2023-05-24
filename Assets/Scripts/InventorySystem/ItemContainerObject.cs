using UnityEngine;

public class ItemContainerObject : BasicItemObject {

    public ItemContainer itemContainer;

    protected override void Awake( ) {

        base.Awake( );
        itemContainer = new ItemContainer();

    }

    public override void SetSpriteIcon( ) {

        spriteIcon = itemContainer.itemContainerData.GetSpriteIcon();
        ChangeIcon( itemContainer.itemContainerData.width, itemContainer.itemContainerData.height );

    }

}
