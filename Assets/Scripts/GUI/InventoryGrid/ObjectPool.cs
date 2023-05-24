using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool {

    private GameObject prefab;

    private int maxObjects;

    private Queue<GameObject> inactiveObjects;
    private HashSet<GameObject> activeObjects;

    public ObjectPool( GameObject prefab, int maxObjects, GameObject father ) {

        this.prefab = prefab;
        this.maxObjects = maxObjects;
        inactiveObjects = new Queue<GameObject>();
        activeObjects = new HashSet<GameObject>();

        Init( father );

    }

    public void Init( GameObject father ) {

        for ( int i = 0; i < maxObjects; i++ ) {

            GameObject obj = Object.Instantiate( prefab );
            obj.GetComponent<RectTransform>().SetParent( father.GetComponent<RectTransform>() );
            obj.SetActive( false );
            inactiveObjects.Enqueue( obj );

        }
    }

    private GameObject CreateObject( ) {

        GameObject obj = Object.Instantiate( prefab );
        obj.SetActive( false );
        return obj;

    }

    public GameObject GetObject( ) {

        GameObject obj;

        if ( inactiveObjects.Count > 0 ) {

            obj = inactiveObjects.Dequeue();

        }
        else {
            Debug.Log("crea");
            obj = CreateObject();

        }

        obj.SetActive( true );
        activeObjects.Add( obj );

        return obj;
    }

    public void ReturnObject( GameObject obj ) {

        obj.SetActive( false );

        activeObjects.Remove( obj );
        inactiveObjects.Enqueue( obj );

    }

}
