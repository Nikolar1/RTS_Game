using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Buildings.Player
{
    public class BuildingBuildManager : MonoBehaviour
    {
        public static BuildingBuildManager instance = null;
        public BasicBuilding selectedBuilding;
        public void Awake()
        {
            instance = this;
        }

        public bool SpawnBuilding()
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!CanSpawnBuilding(worldPoint))
            {
                RTS.Player.VoiceAssistant.instance.PlayConstructionInterupted();
                return false;
            }
            GameObject spawnedObject = Instantiate(selectedBuilding.buildingPrefab, worldPoint, Quaternion.identity);
            PlayerBuilding pB = spawnedObject.GetComponent<PlayerBuilding>();
            pB.baseStats = selectedBuilding.baseStats;
            pB.baseStats = selectedBuilding.baseStats;
            pB.currentHealth = 10;
            pB.attackCooldown = RTS.Player.PlayerManager.instance.attackCooldown;
            pB.currentAttackCooldown = RTS.Player.PlayerManager.instance.attackCooldown;
            spawnedObject.transform.GetComponent<Interactable.IBuilding>().actions = selectedBuilding.actions;
            spawnedObject.transform.GetComponent<BuildingBuildQueue>().actions = selectedBuilding.actions;
            foreach (Transform type in RTS.Player.PlayerManager.instance.playerBuildings)
            {
                string typeName = type.name;
                if (typeName == BuildingHandler.instance.GetBuildingType(selectedBuilding))
                {
                    pB.transform.SetParent(type.transform);
                    break;
                }
            }
            return true;
        }

        private bool CanSpawnBuilding(Vector2 position)
        {
            BoxCollider2D buildingCollider = selectedBuilding.buildingPrefab.GetComponent<BoxCollider2D>();
            if (Physics2D.OverlapBox(position + buildingCollider.offset, buildingCollider.size, 0) != null)
            {
                return false;
            }
            if (selectedBuilding.TakeResources())
            {
                return true;
            }
            return false;
        }

    }
}