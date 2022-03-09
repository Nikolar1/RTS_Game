using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NR.RTS.Buildings
{
    public class BuildingStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float health, attack,  range,  shootingSpeed;
            public int armorPiercing, precission;
        }
    }
}