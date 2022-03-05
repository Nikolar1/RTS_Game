using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Units {

    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        private Unit archer, arqubasier, horseman, slinger, spearman, swordsman, worker;

        public void Awake()
        {
            instance = this;
        }

        public Unit GetUnit(string type)
        {
            Unit unit;
            switch (type)
            {
                case "archer":
                    unit = archer;
                    break;
                case "arqubasier":
                    unit = arqubasier;
                    break;
                case "horseman":
                    unit = horseman;
                    break;
                case "slinger":
                    unit = slinger;
                    break;
                case "spearman":
                    unit = spearman;
                    break;
                case "swordsman":
                    unit = swordsman;
                    break;
                case "worker":
                    unit = worker;
                    break;
                default:
                    Debug.Log($"Unit type {type} not found");
                    unit = null;
                    break;
            }
            return unit;
        }

        public void SetUnitStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string typeName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    Unit baseUnit = GetUnit(typeName);
                    Player.PlayerUnit pU;
                    if (type == NR.RTS.Player.PlayerManager.instance.playerUnits)
                    {
                        pU = unit.GetComponent<Player.PlayerUnit>();
                        pU.cost = baseUnit.cost;
                        pU.armor = baseUnit.armor;
                        pU.defence = baseUnit.defence;
                        pU.health = baseUnit.health;
                        pU.speed = baseUnit.speed;
                        pU.meleeAttack = baseUnit.meleeAttack;
                        pU.meleeArmorPiercing = baseUnit.meleeArmorPiercing;
                        pU.rangedAttack = baseUnit.rangedAttack;
                        pU.rangedArmorPiercing = baseUnit.rangedArmorPiercing;
                        pU.precission = baseUnit.precission;
                        pU.range = baseUnit.range;
                        pU.shootingSpeed = baseUnit.shootingSpeed;
                    }
                    else if (type == NR.RTS.Player.PlayerManager.instance.enemyUnits)
                    {

                    }
                    

                }
            }
        }

    }
}

