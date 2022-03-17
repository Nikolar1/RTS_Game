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

        [SerializeField] private Button buildQueueButton = null;
        [SerializeField] private Transform buildQueuelayoutGroup = null;
        private List<Button> buildQueueButtons = new List<Button>();
        private List<Button> buttons = new List<Button>();
        private PlayerActions actionsList = null;
        public Transform currentSelection;
        public  Buildings.Player.BuildingBuildQueue buildQueue = null;
        public GameObject rallyPoint = null;
        private void Awake()
        {
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions, GameObject rallyPoint, Transform selectedObject)
        {
            actionsList = actions;
            currentSelection = selectedObject;
            buildQueue = currentSelection.GetComponent<Buildings.Player.BuildingBuildQueue> ();
            this.rallyPoint = rallyPoint;
            buildQueue.PauseTimer();
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
            if (buildQueue.buildQueueIndecies.Count>0)
            {
                int i = 0;
                foreach (int index in buildQueue.buildQueueIndecies)
                {
                    if (i == 0)
                    {
                        AddButton(buildQueue.spawnOrder[i], buildQueue.buildQueueIndecies[i], currentSelection, buildQueue.actionTimer.elapsedTime);
                    }
                    else
                    {
                            AddButton(buildQueue.spawnOrder[i], buildQueue.buildQueueIndecies[i], currentSelection);
                    }
                    i++;
                }
            }
            buildQueue.ContinueTimer();
        }

        public void ClearActions()
        {
            foreach (Button btn in buttons)
            {
                Destroy(btn.gameObject);
            }
            foreach (Button btn in buildQueueButtons)
            {
                Destroy(btn.gameObject);
            }
            buttons.Clear();
            buildQueueButtons.Clear();
            currentSelection = null;
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            buildQueue.StartSpawnTimer(objectToSpawn);
        }

        public void RemoveUnitFromBuildQueue(int spawnIndex)
        {
            buildQueue.RemoveUnitFromBuildQueue(spawnIndex);
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

        public void AddButton(Units.Unit unit, int index, Transform askingObject, float elapsedTime = 0)
        {
            if (!askingObject.Equals(currentSelection))
            {
                return;
            }
            Button btn = Instantiate(buildQueueButton, buildQueuelayoutGroup);
            btn.name = "" + index;
            GameObject icon = Instantiate(unit.icon, btn.transform);
            btn.GetComponent<BuildQueueAction>().spawnTimer = unit.spawnTime;
            buildQueueButtons.Add(btn);
            if (buildQueue.spawnOrder.Count == 1)
            {
                buildQueueButtons[0].GetComponent<BuildQueueAction>().StartTimer(elapsedTime);
            }
        }

        public void RemoveButton(int index, Transform askingObject = null)
        {
            if (!askingObject.Equals(currentSelection))
            {
                return;
            }
            Destroy(buildQueueButtons[index].gameObject);
            buildQueueButtons.Remove(buildQueueButtons[index]);
            if (buildQueueButtons.Count>0 && index == 0)
            {
                buildQueueButtons[0].GetComponent<BuildQueueAction>().StartTimer();
            }
        }

        public void SpawnObject()
        {
            GameObject spawnedObject = Instantiate(buildQueue.spawnOrder[0].unitPrefab, rallyPoint.transform.parent.position + Vector3.right, Quaternion.identity);
            Units.Player.PlayerUnit pU = spawnedObject.GetComponent<Units.Player.PlayerUnit>();

            pU.baseStats = buildQueue.spawnOrder[0].baseStats;
            pU.currentHealth = buildQueue.spawnOrder[0].baseStats.health;
            pU.attackCooldown = Player.PlayerManager.attackCooldown;
            pU.currentAttackCooldown = Player.PlayerManager.attackCooldown;
            pU.SetSpeed();
            pU.MoveUnit(rallyPoint.transform.position);
            foreach (Transform type in Player.PlayerManager.instance.playerUnits)
            {
                string typeName = type.name;
                Debug.Log(Units.UnitHandler.instance.GetUnitType(buildQueue.spawnOrder[0]));
                if (typeName == Units.UnitHandler.instance.GetUnitType(buildQueue.spawnOrder[0]))
                {
                    pU.transform.SetParent(type.transform);
                    break;
                }
            }
            RemoveButton(0);
            buildQueue.spawnOrder.Remove(buildQueue.spawnOrder[0]);
            buildQueue.spawnQueue.Remove(buildQueue.spawnQueue[0]);

        }
    }
}