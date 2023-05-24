using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelAllyData  {

    private Sprite allySprite;
    private int allyId;
    private int buttonId;
    private string allyName;
    private string allyLastName;
    private bool isEmpty;

    public void Copy( PanelAllyData ally ) {

        allySprite = ally.allySprite;
        allyId = ally.allyId;
        buttonId = ally.buttonId;
        allyName = ally.allyName;
        allyLastName = ally.allyLastName;
        isEmpty = ally.isEmpty;
        
    }

    public void Set( Sprite sprite, int id, string name, string lastName, bool isEmpty, int buttonId ) {

        allySprite = sprite;
        allyId = id;
        this.buttonId = buttonId;
        allyName = name;
        allyLastName = lastName;
        this.isEmpty= isEmpty;

    }

    public Sprite GetSprite( ) {
    
        return allySprite;
    
    }

    public string GetName( ) {

        return allyName;
    
    }

    public int GetAllyId( ) {

        return allyId;
    
    }

    public bool IsEmpty( ) {
    
        return isEmpty;
    
    }

    public void ChangeIfEmpty( bool isEmpty ) {
    
        this.isEmpty= isEmpty;
    
    }

    public void Clear( ) {

        allySprite = null;
        allyId = 0;
        buttonId = 0;
        allyName = null;
        allyLastName = null;
        isEmpty = true;

    }

}


