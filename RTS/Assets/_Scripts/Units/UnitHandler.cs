using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NR.RTS.Player;

namespace NR.RTS.Units {

    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        private Unit archer, arqubasier, horseman, slinger, spearman, swordsman, worker;

        public LayerMask pUnitLayer, eUnitLayer;

        public const float attackCooldown = 1;

        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
            pUnitLayer = LayerMask.NameToLayer("PlayerUnits");
        }

        public Unit GetUnit(string type)
        {
            Unit unit;
            switch (type)
            {
                case "archers":
                    unit = archer;
                    break;
                case "arqubasiers":
                    unit = arqubasier;
                    break;
                case "horsemen":
                    unit = horseman;
                    break;
                case "slingers":
                    unit = slinger;
                    break;
                case "spearmen":
                    unit = spearman;
                    break;
                case "swordsmen":
                    unit = swordsman;
                    break;
                case "workers":
                    unit = worker;
                    break;
                default:
                    Debug.LogError($"Unit type {type} not found");
                    unit = null;
                    break;
            }
            return unit;
        }

        public void SetUnitStats(Transform type)
        {
            Transform pUnits = PlayerManager.instance.playerUnits;
            Transform eUnits = PlayerManager.instance.enemyUnits;
            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string typeName = child.name.ToLower();
                    Unit baseUnit = GetUnit(typeName);
                    if (baseUnit == null)
                    {
                        baseUnit = worker;
                    }
                    if (type == pUnits)
                    {
                        Player.PlayerUnit pU = unit.GetComponent<Player.PlayerUnit>();
                        pU.cost = baseUnit.cost;
                        pU.armor = baseUnit.armor;
                        pU.defence = baseUnit.defence;
                        pU.health = baseUnit.health;
                        pU.currentHealth = baseUnit.health;
                        pU.speed = baseUnit.speed;
                        pU.meleeAttack = baseUnit.meleeAttack;
                        pU.meleeArmorPiercing = baseUnit.meleeArmorPiercing;
                        pU.rangedAttack = baseUnit.rangedAttack;
                        pU.rangedArmorPiercing = baseUnit.rangedArmorPiercing;
                        pU.precission = baseUnit.precission;
                        pU.range = baseUnit.range;
                        pU.shootingSpeed = baseUnit.shootingSpeed;
                        pU.attackCooldown = attackCooldown;
                        pU.currentAttackCooldown = attackCooldown;
                    }
                    else if (type == eUnits)
                    {
                        Enemy.EnemyUnit eU = unit.GetComponent<Enemy.EnemyUnit>();
                        eU.cost = baseUnit.cost;
                        eU.armor = baseUnit.armor;
                        eU.defence = baseUnit.defence;
                        eU.health = baseUnit.health;
                        eU.currentHealth = baseUnit.health;
                        eU.speed = baseUnit.speed;
                        eU.meleeAttack = baseUnit.meleeAttack;
                        eU.meleeArmorPiercing = baseUnit.meleeArmorPiercing;
                        eU.rangedAttack = baseUnit.rangedAttack;
                        eU.rangedArmorPiercing = baseUnit.rangedArmorPiercing;
                        eU.precission = baseUnit.precission;
                        eU.range = baseUnit.range;
                        eU.shootingSpeed = baseUnit.shootingSpeed;
                        eU.attackCooldown = attackCooldown;
                        eU.currentAttackCooldown = attackCooldown;
                    }   
                    

                }
            }
        }

    }
}

