using UnityEngine;

public class BasicItem {

    public ItemTier itemTier = ItemTier.TierUno;
    public ItemRarity itemRarity = ItemRarity.MuyCom�n;

    public string nombreItem;

    public BasicItemData basicItemData;

}


public enum ItemRarity {

    MuyRaro,
    Raro,
    Normal,
    Com�n,
    MuyCom�n

}

public enum ItemTier {

    TierUno,
    TierDos,
    TierTres,
    TierCuatro,
    TierCinco

}
