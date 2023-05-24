using UnityEngine;

public class BasicItem {

    public ItemTier itemTier = ItemTier.TierUno;
    public ItemRarity itemRarity = ItemRarity.MuyComún;

    public string nombreItem;

    public BasicItemData basicItemData;

}


public enum ItemRarity {

    MuyRaro,
    Raro,
    Normal,
    Común,
    MuyComún

}

public enum ItemTier {

    TierUno,
    TierDos,
    TierTres,
    TierCuatro,
    TierCinco

}
