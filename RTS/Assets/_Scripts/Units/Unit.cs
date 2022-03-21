using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Units {

    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class Unit : ScriptableObject
    {

        public enum unitType { 
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
        public unitType type;
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
            if (RTS.Player.PlayerResourceManager.instance.gold > baseStats.cost)
            {
                RTS.Player.PlayerResourceManager.instance.RemoveGold( baseStats.cost);
                return true;
            }
            return false;
        }

        public void ReturnResources()
        {
            RTS.Player.PlayerResourceManager.instance.AddGold(baseStats.cost);
        }
    }
}


