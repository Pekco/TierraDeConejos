using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    [SerializeField] public Dictionary<string, BodyPart> bodyParts = new Dictionary<string, BodyPart>();

    private void Awake() {

        foreach ( BodyPart bodyPart in transform.parent.GetComponentsInChildren<BodyPart>() ) {

            bodyParts.Add( bodyPart.gameObject.name, bodyPart );

        }

    }

}
