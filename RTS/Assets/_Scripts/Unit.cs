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

        [Space(15)]
        [Header("Unit Settings")]
        public unitType type;
        public new string name;
        public GameObject unitPrefab;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]
        public int cost;
        public int armor;
        public int defence;
        public double health;
        public double speed;

        [Space(15)]
        [Header("Unit Melee Stats")]
        public double meleeAttack;
        public int meleeArmorPiercing;

        [Space(15)]
        [Header("Unit Ranged Stats")]
        public double rangedAttack;
        public int rangedArmorPiercing;
        public int precission;
        public int range;
        public int shootingSpeed;
        

    }
}


