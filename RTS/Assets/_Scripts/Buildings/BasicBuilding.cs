using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum buildingType
        {
            Camp,
            Barracks,
            Tower,
            Wall
        }

        [Space(15)]
        [Header("Building Settings")]

        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public GameObject icon;
        public UI.HUD.PlayerActions actions;

        [Space(15)]
        [Header("Building Base Stats")]
        [Space(40)]

        public BuildingStatTypes.Base baseStats;

        public bool TakeResources()
        {
            if (RTS.Player.PlayerResourceManager.instance.gold > baseStats.cost)
            {
                RTS.Player.PlayerResourceManager.instance.gold -= baseStats.cost;
                return true;
            }
            return false;
        }

        public void ReturnResources()
        {
            RTS.Player.PlayerResourceManager.instance.gold += baseStats.cost;
        }
    }
}


