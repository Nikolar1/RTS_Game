using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Units
{
    public interface Damageable
    {
        public void TakeDamage(float damage, int armorPiercing);
    }
}