using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryData/ ContainerItemData" )]
public class ItemContainerData : BasicItemData {

    // Si se pueden stackear objetos dentro del contenedor.
    public bool canItemsBeStacked;

    // Espacio que proporciona.
    public int spaceWidth;
    public int spaceHeight;

    public float weightReduction {

        get {

            return weightReduction;

        }
        set {

            weightReduction = Mathf.Clamp( value, 0, 1 );

        }

    }

}
