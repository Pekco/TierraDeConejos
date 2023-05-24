using UnityEngine;
using UnityEngine.U2D.Animation;

public class EquipContainer : MonoBehaviour {

    [SerializeField] public EquipableType equipableType;

    SpriteResolver spriteResolver;
    SpriteLibrary spriteLibrary;
    SpriteRenderer spriteRenderer;

    [SerializeField] EquipLibraryAssetsContainer libraryAssets;

    private void Awake( ) {
        
        spriteResolver = GetComponent<SpriteResolver>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void ChangeEquip( BasicItem itemToEquip ) {

        gameObject.SetActive( true );

        foreach ( SpriteLibraryAsset spriteLibraryAsset in libraryAssets.armaduraInferiorAssets ) {

            if ( spriteLibraryAsset.name == itemToEquip.basicItemData.nombreItem ) {

                spriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
            
            }
        
        }
    
    }

    public void Disequip( ) {
    
        spriteLibrary.spriteLibraryAsset = null;
        spriteRenderer.sprite = null;

        gameObject.SetActive( false );
    
    }

}
