using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Buildings.Player
{
    [RequireComponent(typeof(UI.HUD.ActionTimer))]
    public class BuildingBuildQueue : MonoBehaviour
    {
        public List<int> buildQueueIndecies = new List<int>();
        public int buildQueueIndex = 0;
        public List<float> spawnQueue = new List<float>();
        public List<Units.Unit> spawnOrder = new List<Units.Unit>();
        public UI.HUD.ActionTimer actionTimer;
        public UI.HUD.PlayerActions actions;
        public GameObject rallyPoint;

        private void Awake()
        {
            actionTimer = GetComponent<UI.HUD.ActionTimer>();
        }

        public void StartSpawnTimer(string objectToSpawn)
        {
            if (IsUnit(objectToSpawn))
            {

                Units.Unit unit = IsUnit(objectToSpawn);
                if (unit.TakeResources())
                {
                    spawnQueue.Add(unit.spawnTime);
                    spawnOrder.Add(unit);
                    buildQueueIndecies.Add(buildQueueIndex);
                    UI.HUD.ActionFrame.instance.AddButton(unit, buildQueueIndex, transform);
                    buildQueueIndex++;
                    RTS.Player.PlayerResourceManager.instance.RemoveGold(unit.baseStats.cost);
                }
            }
            else
            {
                Debug.LogError($"{objectToSpawn} is not a spawnable object");
            }

            if (spawnQueue.Count == 1)
            {
                actionTimer.StartTimer(spawnOrder[0].spawnTime);
            }
        }
        public void SpawnObject()
        {
            GameObject spawnedObject = Instantiate(spawnOrder[0].unitPrefab, rallyPoint.transform.parent.position + Vector3.right, Quaternion.identity);
            Units.Player.PlayerUnit pU = spawnedObject.GetComponent<Units.Player.PlayerUnit>();

            pU.baseStats = spawnOrder[0].baseStats;
            pU.currentHealth = spawnOrder[0].baseStats.health;
            pU.attackCooldown = RTS.Player.PlayerManager.instance.attackCooldown;
            pU.currentAttackCooldown = RTS.Player.PlayerManager.instance.attackCooldown;
            pU.SetSpeed();
            spawnedObject.GetComponent<SpriteRenderer>().sprite = spawnOrder[0].icon.transform.GetChild(0).GetComponent<Image>().sprite;
            pU.MoveUnit(rallyPoint.transform.position);
            spawnedObject.transform.GetComponent<Interactable.IUnit>().actions = spawnOrder[0].actions;
            foreach (Transform type in RTS.Player.PlayerManager.instance.playerUnits)
            {
                string typeName = type.name;
                Debug.Log(Units.UnitHandler.instance.GetUnitType(spawnOrder[0]));
                if (typeName == Units.UnitHandler.instance.GetUnitType(spawnOrder[0]))
                {
                    pU.transform.SetParent(type.transform);
                    break;
                }
            }
            UI.HUD.ActionFrame.instance.RemoveButton(0, transform);
            spawnOrder.Remove(spawnOrder[0]);
            spawnQueue.Remove(spawnQueue[0]);
            buildQueueIndecies.Remove(buildQueueIndecies[0]);
            if (spawnQueue.Count > 0)
            {
                actionTimer.StartTimer(spawnOrder[0].spawnTime);
            }
            if (spawnQueue.Count == 0)
            {
                actionTimer.StopTimer();
            }
        }

        private Units.Unit IsUnit(string name)
        {
            if (actions.units.Count > 0)
            {
                foreach (Units.Unit unit in actions.units)
                {
                    if (unit.name == name)
                    {
                        return unit;
                    }
                }
            }

            return null;
        }

        public void RemoveUnitFromBuildQueue(int spawnIndex)
        {
            int index = buildQueueIndecies.IndexOf(spawnIndex);
            UI.HUD.ActionFrame.instance.RemoveButton(index, transform);
            if (index != 0)
            {
                spawnOrder[index].ReturnResources();
                spawnOrder.Remove(spawnOrder[index]);
                spawnQueue.Remove(spawnQueue[index]);
                buildQueueIndecies.Remove(buildQueueIndecies[index]);
            }
            else
            {
                actionTimer.StopTimer();
                spawnOrder[0].ReturnResources();
                spawnOrder.Remove(spawnOrder[0]);
                spawnQueue.Remove(spawnQueue[0]);
                buildQueueIndecies.Remove(buildQueueIndecies[0]);
                if (spawnQueue.Count > 0)
                {
                    actionTimer.StartTimer(spawnOrder[0].spawnTime);
                }
            }

        }

        public void PauseTimer()
        {
            actionTimer.StopTimer();
        }

        public void ContinueTimer()
        {
            actionTimer.ContinueTimer();
        }
    }
}
