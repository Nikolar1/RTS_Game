using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Units {

    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class Unit : ScriptableObject
    {

        public enum UnitType { 
            Worker,
            Spearmen,
            Swordsman,
            Archer,
            Slinger,
            Aquabasier,
            Horseman
        };

        [Space(15)]
        [Header("Unit Settings")]
        public UnitType type;
        public new string name;
        public GameObject unitPrefab;
        public GameObject icon;
        public float spawnTime;
        public UI.HUD.PlayerActions actions;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]
        public UnitStats.Base baseStats;


        public bool TakeResources()
        {

            if (RTS.Player.PlayerResourceManager.instance.GetGoldAmmount() > baseStats.cost && RTS.Player.PlayerResourceManager.instance.CheckPopulation(baseStats.populationType))
            {
                RTS.Player.PlayerResourceManager.instance.RemoveGold( baseStats.cost);
                RTS.Player.PlayerResourceManager.instance.RemovePopulation(baseStats.populationType);
                return true;
            }
            return false;
        }

        public void ReturnResources()
        {
            RTS.Player.PlayerResourceManager.instance.AddGold(baseStats.cost);
            RTS.Player.PlayerResourceManager.instance.AddPopulation(baseStats.populationType);
        }
    }
}


