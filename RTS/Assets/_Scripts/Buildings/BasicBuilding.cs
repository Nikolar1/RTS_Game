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
            Camp
        }

        [Space(15)]
        [Header("Building Settings")]

        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;

        [Space(15)]
        [Header("Building Base Stats")]
        [Space(40)]

        public BuildingStatTypes.Base baseStats;
    }
}


