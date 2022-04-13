using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum BuildingType
        {
            Camp,
            Barracks,
            Tower,
            Wall
        }

        [Space(15)]
        [Header("Building Settings")]

        public BuildingType type;
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
            if (RTS.Player.PlayerResourceManager.instance.GetGoldAmmount() > baseStats.cost)
            {
                RTS.Player.PlayerResourceManager.instance.RemoveGold(baseStats.cost);
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


