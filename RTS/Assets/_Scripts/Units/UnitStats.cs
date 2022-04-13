using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Units
{
    public class UnitStats : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public enum UnitPopulationType
            {
                TierOne,
                TierTwo
            }

            public int cost;
            public UnitPopulationType populationType;
            public int armor;
            public int defence;
            public float health;
            public float speed;
            public float range;
            public bool canRepair;
            public bool canWork;

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
}
