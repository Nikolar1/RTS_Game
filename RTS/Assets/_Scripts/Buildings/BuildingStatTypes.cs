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
            public float health, attack,  range,  shootingSpeed, cost;
            public int armorPiercing, precission;
            public AudioClip[] damagedSounds;
            public AudioClip[] firingSounds;
        }
    }
}