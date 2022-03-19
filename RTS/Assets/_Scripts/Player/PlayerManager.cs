using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NR.RTS.InputManager;

namespace NR.RTS.Player
{
    
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;
        public Transform playerBuildings;

        private bool isInBuildMode = false;

        public float attackCooldown = 1;
        public float buildSpeed = 100;
        public float timeBetweenTargetChecks = 5;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            SetUnitStats(playerUnits);
            SetUnitStats(enemyUnits);
            SetUnitStats(playerBuildings);
        }

        // Update is called once per frame
        void Update()
        {
            if (isInBuildMode)
            {
                InputHandler.instance.HandleBuildingPlacement();
            }
            else
            {
                InputHandler.instance.HandleUnitMovment();
            }
            InputHandler.instance.HandleCameraMovment();
        }

        public void EnterBuildMode()
        {
            isInBuildMode = true;
        }
        public void ExitBuildMode()
        {
            isInBuildMode = false;
        }
        public void SetUnitStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string typeName = child.name.ToLower();
                    if (type == playerBuildings)
                    {
                        Buildings.BasicBuilding basicBuilding = Buildings.BuildingHandler.instance.GetBuilding(typeName);
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = basicBuilding.baseStats;
                        pB.currentHealth = pB.baseStats.health;
                        pB.attackCooldown = attackCooldown;
                        pB.currentAttackCooldown = attackCooldown;
                        pB.isBuilt = true;
                        tf.GetComponent<Interactable.IBuilding>().actions = basicBuilding.actions;
                        tf.GetComponent<Buildings.Player.BuildingBuildQueue>().actions = basicBuilding.actions;
                    }
                    else
                    {
                        Units.Unit baseUnit = Units.UnitHandler.instance.GetUnit(typeName);
                        if (baseUnit == null)
                        {
                            baseUnit = Units.UnitHandler.instance.worker;
                        }
                        if (type == playerUnits)
                        {
                            Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                            pU.baseStats = baseUnit.baseStats;
                            pU.currentHealth = baseUnit.baseStats.health;
                            pU.attackCooldown = attackCooldown;
                            pU.currentAttackCooldown = attackCooldown;
                            pU.SetSpeed();
                            tf.GetComponent<Interactable.IUnit>().actions = baseUnit.actions;
                        }
                        else if (type == enemyUnits)
                        {
                            Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                            eU.baseStats = baseUnit.baseStats;
                            eU.currentHealth = baseUnit.baseStats.health;
                            eU.attackCooldown = attackCooldown;
                            eU.currentAttackCooldown = attackCooldown;
                            eU.SetSpeed();
                        }
                    }


                }
            }
        }
    }

}
