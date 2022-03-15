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

        public const float attackCooldown = 1;

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
            InputHandler.instance.HandleUnitMovment();
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
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.instance.GetBuilding(typeName);
                        pB.currentHealth = pB.baseStats.health;
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
                        }
                        else if (type == enemyUnits)
                        {
                            Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                            eU.baseStats = baseUnit.baseStats;
                            eU.currentHealth = baseUnit.baseStats.health;
                            eU.attackCooldown = attackCooldown;
                            eU.currentAttackCooldown = attackCooldown;
                        }
                    }


                }
            }
        }
    }

}
