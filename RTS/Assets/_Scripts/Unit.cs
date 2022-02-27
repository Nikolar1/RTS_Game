using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Units {

    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class Unit : ScriptableObject
    {
        public enum unitType { 
            Worker,
            Spearmen,
            Swordsman,
            Archer,
            Slinger,
            Aquabasier,
            Horseman
        };

        public bool isPlayerUnit;

        public unitType type;

        public new string name;

        public GameObject unitPrefab;

        public int cost;
        public double meleeAttack;
        public int meleeArmorPiercing;
        public double rangedAttack;
        public int rangedArmorPiercing;
        public int precission;
        public int range;
        public int shootingSpeed;
        public int armor;
        public int defence;
        public double health;
        public double speed;

    }
}


