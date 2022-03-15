using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.UI.HUD
{

    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;
        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();
        private PlayerActions actionsList = null;

        public List<float> spawnQueue = new List<float>();
        public List<Units.Unit> spawnOrder = new List<Units.Unit>();

        public GameObject rallyPoint = null;
        private void Awake()
        {
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions, GameObject rallyPoint)
        {
            actionsList = actions;
            this.rallyPoint = rallyPoint;
            if (actions.units.Count>0)
            {
                foreach (Units.Unit unit in actions.units)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    GameObject icon = Instantiate(unit.icon, btn.transform);
                    buttons.Add(btn);
                }
            }

            if (actions.basicBuildings.Count > 0)
            {
                foreach (Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = building.name;
                    GameObject icon = Instantiate(building.icon, btn.transform);
                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            foreach (Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {

                Units.Unit unit = IsUnit(objectToSpawn);
                spawnQueue.Add(unit.spawnTime);
                spawnOrder.Add(unit);
            }
           /* else if (IsBuilding(objectToSpawn))
            {
                Buildings.BasicBuilding building = IsBuilding(objectToSpawn);
                spawnQueue.Add(0);
                spawnOrder.Add(building.buildingPrefab);
            }*/
            else
            {
                Debug.LogError($"{objectToSpawn} is not a spawnable object");
            }

            if (spawnQueue.Count == 1)
            {
                ActionTimer.instance.StartCoroutine(ActionTimer.instance.SpawnQueueTimer());
            }
            else if (spawnQueue.Count == 0)
            {
                ActionTimer.instance.StopAllCoroutines();
            }
        }

        private Units.Unit IsUnit(string name)
        {
            if (actionsList.units.Count > 0)
            {
                foreach (Units.Unit unit in actionsList.units)
                {
                    if (unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            
            return null;
        }

        private Buildings.BasicBuilding IsBuilding(string name)
        {
            if (actionsList.basicBuildings.Count > 0)
            {
                foreach (Buildings.BasicBuilding building in actionsList.basicBuildings)
                {
                    if (building.name == name)
                    {
                        return building;
                    }
                }
            }
            return null;
        }

        public void SpawnObject()
        {
            GameObject spawnedObject = Instantiate(spawnOrder[0].unitPrefab, rallyPoint.transform.parent.position + Vector3.right, Quaternion.identity);
            Units.Player.PlayerUnit pU = spawnedObject.GetComponent<Units.Player.PlayerUnit>();

            pU.baseStats = spawnOrder[0].baseStats;
            pU.currentHealth = spawnOrder[0].baseStats.health;
            pU.attackCooldown = Player.PlayerManager.attackCooldown;
            pU.currentAttackCooldown = Player.PlayerManager.attackCooldown;
            pU.MoveUnit(rallyPoint.transform.position);
            foreach (Transform type in Player.PlayerManager.instance.playerUnits)
            {
                string typeName = type.name;
                Debug.Log(Units.UnitHandler.instance.GetUnitType(spawnOrder[0]));
                if (typeName == Units.UnitHandler.instance.GetUnitType(spawnOrder[0]))
                {
                    pU.transform.SetParent(type.transform);
                    break;
                }
            }
            spawnOrder.Remove(spawnOrder[0]);
            spawnQueue.Remove(spawnQueue[0]);

        }
    }
}