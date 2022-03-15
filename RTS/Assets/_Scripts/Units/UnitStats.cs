using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : ScriptableObject
{
    [System.Serializable]
    public class Base
    {
        public int cost;
        public int armor;
        public int defence;
        public float health;
        public float speed;
        public float range;

        [Space(15)]
        [Header("Unit Melee Stats")]
        public float meleeAttack;
        public int meleeArmorPiercing;

        [Space(15)]
        [Header("Unit Ranged Stats")]
        public float rangedAttack;
        public int rangedArmorPiercing;
        public int precission;
        public float shootingSpeed;
    }
    
}
