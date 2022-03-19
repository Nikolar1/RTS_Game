using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;
        [SerializeField]
        private BasicBuilding camp,barracks,tower,wall;

        public void Awake()
        {
            instance = this;
        }

        public BasicBuilding GetBuilding(string type)
        {
            BasicBuilding building;
            switch (type.ToLower())
            {
                case "camp":
                    building = camp;
                    break;
                case "barracks":
                    building = barracks;
                    break;
                case "towers":
                    building = tower;
                    break;
                case "walls":
                    building = wall;
                    break;
                default:
                    Debug.LogError($"Unit type {type} not found");
                    return null;
                    break;
            }
            return building;
        }

        public string GetBuildingType(BasicBuilding building)
        {
            if (building.type == camp.type)
            {
                return "Camp";
            }
            else if (building.type == barracks.type)
            {
                return "Barracks";
            }
            else if (building.type == tower.type)
            {
                return "Towers";
            }
            else if (building.type == wall.type)
            {
                return "Walls";
            }
            return null;
        }
    }
}