using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour {

    [SerializeField] GameObject highlighter;
    [SerializeField] private float distanceOnObject;

    GameObject currentTarget;

    private void Awake( ) {

        distanceOnObject = 25.0f;

    }
    public void Highlight( GameObject target ) {

        if ( currentTarget == target ) {
            return;
        }

        currentTarget = target;
        Vector3 position = target.transform.position + Vector3.up * distanceOnObject;
        Highlight( position );

    }

    public void Highlight( Vector3 position ) {

        highlighter.SetActive( true );
        highlighter.transform.position = position;

    }

    public void Hide( ) {

        currentTarget = null;
        highlighter.SetActive( false );

    }

}
