using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "InventoryGridData/ GridData" )]
public class GridData : ScriptableObject {

    public const int tileSizeWidth = 33;
    public const int tileSizeHeight = 33;

    public int width;
    public int height;

    public bool itemsCanBeStacked;


    public EquipableType equipableType;
    public GridType gridType;

}

public enum GridType {

    Normal,
    Main

}
