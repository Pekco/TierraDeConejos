using UnityEngine;

public class ItemObject : BasicItemObject {

    public Item item;

    protected override void Awake( ) {

        base.Awake();
        item = new Item();

    }

    public override void SetSpriteIcon( ) {

        spriteIcon = item.itemData.GetSpriteIcon();
        ChangeIcon( item.itemData.width, item.itemData.height );

    }

}
