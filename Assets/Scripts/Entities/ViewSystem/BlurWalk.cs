using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlurWalk : MonoBehaviour {

    Light2D blur;
    CharacterControllerAI controllerAI;

    bool upIntensity;
    private const float blurCoef = 0.01f;

    private void Awake( ) {
        
        blur = GetComponent<Light2D>();
        controllerAI = transform.parent.GetComponent<CharacterControllerAI>();

    }

    private void Update( ) {

        if ( controllerAI.IsMoving() ) { 

            if ( upIntensity ) {

                blur.falloffIntensity += blurCoef;

            }
            else {

                blur.falloffIntensity -= blurCoef;

            }

        }
        else {

            blur.falloffIntensity = 0.5f;
        
        }

        if ( blur.falloffIntensity <= 0.5 ) {

            upIntensity = true;

        }
        else if ( blur.falloffIntensity == 1 ) {
        
            upIntensity = false;
        
        }

    }

}
