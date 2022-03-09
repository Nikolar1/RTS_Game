using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;
        [SerializeField]
        private BasicBuilding camp;

        public void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetBuilding(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "camp":
                    building = camp;
                    break;
                default:
                    Debug.LogError($"Unit type {type} not found");
                    return null;
                    break;
            }
            return building.baseStats;
        }

        
    }
}