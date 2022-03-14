using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NR.RTS.Player;

namespace NR.RTS.Units {

    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        public Unit archer, arqubasier, horseman, slinger, spearman, swordsman, worker;

        public LayerMask pUnitLayer, eUnitLayer;

        public const float attackCooldown = 1;

        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
            pUnitLayer = LayerMask.NameToLayer("Interactables");
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

        public string GetUnitType(Unit unit)
        {
            if (unit.type == archer.type)
            {
                return "Archers";
            }
            else if (unit.type == arqubasier.type)
            {
                return "Arqubasiers";
            }
            else if (unit.type == horseman.type)
            {
                return "Horsemen";
            }
            else if (unit.type == slinger.type)
            {
                return "Slingers";
            }
            else if (unit.type == spearman.type)
            {
                return "Spearmen";
            }
            else if (unit.type == swordsman.type)
            {
                return "Swordsmen";
            }
            else if (unit.type == worker.type)
            {
                return "Workers";
            }
            return "";
        }

    }
}

