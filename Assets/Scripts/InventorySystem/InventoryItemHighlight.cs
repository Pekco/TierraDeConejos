using UnityEngine;

public class InventoryItemHighlight : MonoBehaviour{

    [SerializeField] public RectTransform highlighter;
    
    public void SetSizeHighlighter( BasicItem item ) {
        
        Vector2 size = Vector2.zero;

        size.x = item.basicItemData.width * GridData.tileSizeWidth;
        size.y = item.basicItemData.height * GridData.tileSizeHeight;

        highlighter.sizeDelta = size;

    }

    public void SetPosHighlighter( Vector2 origin, Vector3 gridPos ) {
        
        Vector2 pos = Vector2.zero;

        pos.x = origin.x * GridData.tileSizeWidth;
        pos.y = - origin.y * GridData.tileSizeHeight;

        highlighter.localPosition = pos;

    }

    public void ShowHighlighter( bool show ) {

        highlighter.gameObject.SetActive( show );

    }

}
