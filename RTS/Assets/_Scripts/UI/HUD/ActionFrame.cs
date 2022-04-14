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

        public void SetActionButtons(PlayerActions actions, Transform selectedObject, GameObject rallyPoint = null)
        {
            if (actions == null)
            {
                return;
            }
            actionsList = actions;
            currentSelection = selectedObject;
            this.rallyPoint = rallyPoint;
            if (actions.units.Count>0)
            {

                buildQueue = currentSelection.GetComponent<Buildings.Player.BuildingBuildQueue>();
                buildQueue.PauseTimer();
                foreach (Units.Unit unit in actions.units)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    GameObject icon = Instantiate(unit.icon, btn.transform);
                    buttons.Add(btn);
                }
                if (buildQueue.buildQueueIndecies.Count > 0)
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

        public void ClearActions(Transform askingObject)
        {
            if (!askingObject.Equals(currentSelection))
            {
                return;
            }
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
            if (buildQueue != null && IsUnit(objectToSpawn))
            {
                buildQueue.StartSpawnTimer(objectToSpawn);
            }
            else if (IsBuilding(objectToSpawn))
            {

                Buildings.BasicBuilding building = IsBuilding(objectToSpawn);
                Buildings.Player.BuildingBuildManager.instance.selectedBuilding = building;
                Player.PlayerManager.instance.EnterBuildMode();
            }
            else
            {
                Debug.LogError($"{objectToSpawn} is not a spawnable object");
            }

        }

        public void RemoveUnitFromBuildQueue(int spawnIndex)
        {
            buildQueue.RemoveUnitFromBuildQueue(spawnIndex);
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

        public void AddButton(Units.Unit unit, int index, Transform askingObject, float elapsedTime = 0)
        {
            if (!askingObject.Equals(currentSelection))
            {
                return;
            }
            Button btn = Instantiate(buildQueueButton, buildQueuelayoutGroup);
            btn.name = "" + index;
            GameObject icon = Instantiate(unit.icon, btn.transform);
            btn.transform.GetChild(0).transform.SetAsLastSibling();
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
    }
}