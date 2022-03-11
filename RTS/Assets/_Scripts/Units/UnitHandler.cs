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

    }
}

