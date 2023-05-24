using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This data is for texts in the Inventory GUI.
/// </summary>
public class InventoryData : ScriptableObject {

    public Dictionary<int, string> typeOfInventory = new Dictionary<int, string>() {

        {0, "Casco" },
        {1, "Máscara" },

        {2, "Camiseta"},
        {3, "Gambesón" },
        {4, "Pechera" },
        {5, "Sobretodo" },

        {6, "Hombreras" },
        {7, "Coderas" },
        {8, "Guantes" },

        {9, "Cinturón" },
        {10, "Pantalones" },
        {11, "Armadura baja" },
        {12, "Coquillas" },
        {13, "Rodilleras" },
        {14, "Calzado" },

        {15, "Arma 1" },
        {16, "Arma 2" },

        {17, "Arma 3" },
        {18, "Arma 4" }

    };

}
