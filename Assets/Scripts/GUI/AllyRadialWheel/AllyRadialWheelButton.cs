using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AllyRadialWheelButton : MonoBehaviour, IPointerClickHandler {

    [SerializeField] BasicPanel panel;

    [SerializeField] Image buttonImage;
    [SerializeField] Text orderText;

    [SerializeField] int myIndex;

    [SerializeField] bool activeClick;

    private void Awake( ) {

        panel = transform.parent.GetComponent<BasicPanel>();

    }

    public void SetIndex(int index ) {
    
        myIndex= index;
    
    }

    public int GetIndex( ) {
    
        return myIndex;
    
    }

    public void SetTextOrder( string order ) {

        orderText.text = order;
    
    }

    public void SetActive( bool isActive ) {
    
        activeClick = isActive;
    
    }

    public void OnPointerClick( PointerEventData eventData ) {

        SetActive( !activeClick );

        if ( activeClick ) {

            buttonImage.color = Color.red;

        }
        else {

            buttonImage.color = Color.white;
        
        }

        panel.OnClick( myIndex, activeClick );
         
    }

}
