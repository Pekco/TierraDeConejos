using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu( menuName = "InventoryData/ ArmorItemData" )]
public class ArmorItemData : ItemData {

    public SpriteLibraryAsset spriteLibraryA;
    public SpriteLibraryAsset spriteLibraryB;
    public SpriteResolver spriteResolver;

    public void ChangeSpriteLibrary( ) {
        // Cambiar la librería de sprites a spriteLibraryB
        //spriteResolver.spriteLibrary = spriteLibraryB;
    }

}
