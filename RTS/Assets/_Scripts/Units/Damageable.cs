using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    public void TakeDamage(float damage, int armorPiercing);
}
