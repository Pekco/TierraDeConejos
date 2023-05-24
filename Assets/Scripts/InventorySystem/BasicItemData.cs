using UnityEngine;

public class BasicItemData : ScriptableObject {

    // Espacio que ocupa en un grid.
    public int width;
    public int height;

    [SerializeField] public ItemType itemType;
    [SerializeField] public EquipableType equipableType;

    [SerializeField] private Sprite inventoryItemIcon;

    public bool stackable;

    public float weight;

    public string nombreItem = " ";

    public Sprite GetSpriteIcon( ) {

        return inventoryItemIcon;
    
    }

}


public enum ItemType {

    MeleWeapon,
    RangedWeapon,
    FireArm,
    Shield,
    Armor,
    Consumable,
    Container,
    Cloth,
    All,
    ArmItem

}

public enum EquipableType {

    Casco = 0,
    Máscara = 1,

    Camiseta = 2,
    Gambesón = 3,
    Pechera = 4,
    Sobretodo = 5,

    HombreraD = 6,
    HombreraI = 7,
    CoderaD = 8,
    CoderaI = 9,
    GuanteD = 10,
    GuanteI = 11,

    Cinturón = 12,
    Pantalones = 13,
    ArmaduraInferior = 14,
    Coquilla = 15,
    CalzadoD = 16,
    CalzadoI = 17,

    Arm1 = 18,
    Arm2 = 19,
    Espalda = 20,

    Main1 = 21,
    Main2 = 22,
    Main3 = 23,
    Main4 = 24,
    Main5 = 25,
    Main6 = 26,
    Main7 = 27,
    Main8 = 28,
    Main9 = 29,
    Main10 = 30

}
