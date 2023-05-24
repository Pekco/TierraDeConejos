using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCharacter {

    private Dictionary<string, InventoryContainer> containers;
    private Dictionary<string, InventoryContainer> mainContainers;

    public InventoryCharacter( ) {

        containers = new Dictionary<string, InventoryContainer> {

            { EquipableType.Casco.ToString(), new InventoryContainer( 1, 1, EquipableType.Casco, ItemType.Armor, this, true ) },
            { EquipableType.Máscara.ToString(), new InventoryContainer( 1, 1, EquipableType.Máscara, ItemType.Armor,this, true ) },
            { EquipableType.Camiseta.ToString(), new InventoryContainer( 2, 2, EquipableType.Camiseta, ItemType.Cloth,this, true ) },
            { EquipableType.Gambesón.ToString(), new InventoryContainer( 2, 3, EquipableType.Gambesón, ItemType.Armor,this, true ) },
            { EquipableType.Pechera.ToString(), new InventoryContainer( 2, 2, EquipableType.Pechera, ItemType.Armor,this, true ) },
            { EquipableType.Sobretodo.ToString(), new InventoryContainer( 2, 3, EquipableType.Sobretodo, ItemType.Cloth,this, true ) },
            { EquipableType.HombreraD.ToString(), new InventoryContainer( 1, 1, EquipableType.HombreraD, ItemType.Armor,this, true ) },
            { EquipableType.HombreraI.ToString(), new InventoryContainer( 1, 1, EquipableType.HombreraI, ItemType.Armor,this, true ) },
            { EquipableType.CoderaD.ToString(), new InventoryContainer( 1, 2, EquipableType.CoderaD, ItemType.Armor,this, true ) },
            { EquipableType.CoderaI.ToString(), new InventoryContainer( 1, 2, EquipableType.CoderaI, ItemType.Armor,this, true ) },
            { EquipableType.GuanteD.ToString(), new InventoryContainer( 1, 1, EquipableType.GuanteD, ItemType.Armor,this, true ) },
            { EquipableType.GuanteI.ToString(), new InventoryContainer( 1, 1, EquipableType.GuanteI, ItemType.Armor,this, true ) },
            { EquipableType.Cinturón.ToString(), new InventoryContainer( 3, 1, EquipableType.Cinturón, ItemType.Container,this, true ) },
            { EquipableType.Pantalones.ToString(), new InventoryContainer( 2, 3, EquipableType.Pantalones, ItemType.Container,this, true ) },
            { EquipableType.ArmaduraInferior.ToString(), new InventoryContainer( 2, 3, EquipableType.ArmaduraInferior, ItemType.Armor,this, true ) },
            { EquipableType.Coquilla.ToString(), new InventoryContainer( 2, 1, EquipableType.Coquilla, ItemType.Armor,this, true ) },
            { EquipableType.CalzadoD.ToString(), new InventoryContainer( 1, 1, EquipableType.CalzadoD, ItemType.Armor,this, true ) },
            { EquipableType.CalzadoI.ToString(), new InventoryContainer( 1, 1, EquipableType.CalzadoI, ItemType.Armor,this, true ) },
            { EquipableType.Arm1.ToString(), new InventoryContainer( 3, 3, EquipableType.Arm1, ItemType.ArmItem,this, true ) },
            { EquipableType.Arm2.ToString(), new InventoryContainer( 3, 3, EquipableType.Arm2, ItemType.ArmItem,this, true ) },
            { EquipableType.Espalda.ToString(), new InventoryContainer( 3, 3, EquipableType.Espalda, ItemType.Container,this, true ) },

        };

        mainContainers = new Dictionary<string, InventoryContainer> {             
            
            { EquipableType.Main1.ToString(), new InventoryContainer( 0, 0, EquipableType.Main1, ItemType.All,this, false ) },
            { EquipableType.Main2.ToString(), new InventoryContainer( 0, 0, EquipableType.Main2, ItemType.All,this, false ) },
            { EquipableType.Main3.ToString(), new InventoryContainer( 0, 0, EquipableType.Main3, ItemType.All,this, false ) },
            { EquipableType.Main4.ToString(), new InventoryContainer( 0, 0, EquipableType.Main4, ItemType.All,this, false ) },
            { EquipableType.Main5.ToString(), new InventoryContainer( 0, 0, EquipableType.Main5, ItemType.All,this, false ) },
            { EquipableType.Main6.ToString(), new InventoryContainer( 0, 0, EquipableType.Main6, ItemType.All,this, false ) },
            { EquipableType.Main7.ToString(), new InventoryContainer( 0, 0, EquipableType.Main7, ItemType.All,this, false ) },
            { EquipableType.Main8.ToString(), new InventoryContainer( 0, 0, EquipableType.Main8, ItemType.All,this, false ) },
            { EquipableType.Main9.ToString(), new InventoryContainer( 0, 0, EquipableType.Main9, ItemType.All,this, false ) },
            { EquipableType.Main10.ToString(), new InventoryContainer( 0, 0, EquipableType.Main10, ItemType.All,this, false ) }

        };

    }

    public InventoryContainer SetMainContainerFromItemContainer( ItemContainer container ) {

        foreach ( InventoryContainer characterContainer in mainContainers.Values ) {

            if ( !characterContainer.ItsUsed() ) {

                characterContainer.ChangeSpace( container.itemContainerData.spaceWidth, container.itemContainerData.spaceHeight );
                characterContainer.SetUsed( true );
                characterContainer.SetItsUsedBy( container );
                return characterContainer;
            
            }
        
        }
        return null;
    
    }

    public InventoryContainer RemoveMainContainerFromItemContainer( ItemContainer container ) {

        foreach ( InventoryContainer characterContainer in mainContainers.Values ) {

            if ( characterContainer.ItsUsed() && characterContainer.GetItsUsedBy() == container ) {

                characterContainer.ChangeSpace( 0, 0 );
                characterContainer.SetUsed( false );
                return characterContainer;

            }

        }
        return null;

    }

    public Dictionary<string, InventoryContainer> GetContainers( ) {

        return containers;
    
    }

    public Dictionary<string, InventoryContainer> GetMainContainers( ) {
    
        return mainContainers;
    
    }

}