using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Options data for localizations.
/// </summary>
[CreateAssetMenu( menuName = "Data/NPC Menu Option Data" )]
public class NPCMenuOptionDataContainer : ScriptableObject {

    public Dictionary<int, string> typeOfEntity = new Dictionary<int, string>() {

        {0, "Neutral" },
        {1, "Ally"},
        {2, "Faction Ally" },
        {3, "Enemy" },
        {4, "Hostile" },
        {5, "Group Ally" }
        
    };

    public Dictionary<int, string> options = new Dictionary<int, string>() {

        {0, "Hablar" },
        {1, "Añadir a la facción"},
        {2, "Eliminar de la facción" },
        {3, "Añadir al grupo" },
        {4, "Eliminar del grupo" }

    };

    public Dictionary<string, int> optionsAux = new Dictionary<string, int>(); // It is the same as options but it returns the strings instead of int. Does it have sense? I don't think so.
    public Dictionary<string, string[]> menus = new Dictionary<string, string[]>();

    public void Init( ) {

        for(int i = 0; i < options.Count; i++) {

            optionsAux.Add( options[i], i );
        
        }

        menus.Add( typeOfEntity[0], new string[] {

            options[0], 
            options[1]

            }

        );

        menus.Add( typeOfEntity[2], new string[] {

            options[0],
            options[2],
            options[3]

            }

        );

        menus.Add( typeOfEntity[5], new string[] {

            options[0],
            options[2],
            options[4]

            }

        );

    }

}


