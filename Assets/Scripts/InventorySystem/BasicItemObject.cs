using UnityEngine;
using UnityEngine.UI;

public class BasicItemObject : MonoBehaviour {

    protected Sprite spriteIcon;
    protected Image icon;

    protected RectTransform rectTransform;

    protected virtual void Awake( ) {
        
        icon = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        icon.sprite = spriteIcon;

    }

    public virtual void SetSpriteIcon( ) {
    

    }

    public void ChangeIcon( int width, int height ) {

        icon.sprite = spriteIcon;
        rectTransform.sizeDelta = new Vector2( width * GridData.tileSizeWidth, height * GridData.tileSizeHeight );
    
    }

}
