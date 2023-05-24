using UnityEngine;
using UnityEngine.UI;

public class StatusLifeBar : MonoBehaviour {

    private Image lifeBar;
    [SerializeField] private BodyPart bodyPart;

    private void Awake( ) {
        
        lifeBar = GetComponent<Image>();

    }

    private void Start( ) {

        bodyPart.onPlayerHealthChanged += ChangeBar;

    }

    private void OnEnable( ) {

        bodyPart.onPlayerHealthChanged += ChangeBar;

    }

    private void OnDisable( ) {
        
        bodyPart.onPlayerHealthChanged -= ChangeBar;

    }

    private void ChangeBar( float amount ) {

        lifeBar.fillAmount = amount;
    
    }

}
