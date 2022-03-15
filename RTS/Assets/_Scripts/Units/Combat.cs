using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Units
{
    public static class Combat
    {
        public static float TakeDamage(float damage, int armorPiercing, int armor, int defence, float currentHealth)
        {
            float totalDamage = (damage - (armor * ((1f * armorPiercing) / 100))) * ((100f - defence) / 100);
            if (totalDamage < 0)
            {
                totalDamage = 0;
            }
            return currentHealth - totalDamage;
        }

        public static bool HandleHealth(Image healthBarAmount, float currentHealth, float health)
        {
            healthBarAmount.fillAmount = currentHealth / health;
            if (currentHealth <= 0)
            {
                return true;
            }
            return false;
        }
        public static float Attack(float currentAttackCooldown, float distance, float attackCooldown, Damageable aggroUnit, UnitStats.Base baseStats)
        {
            if(aggroUnit == null)
            {
                return -2;
            }
            //1.4 is set as the base range because  the distance might be greater than 1.2 when the units meet diagonaly
            if (currentAttackCooldown <= 0 && distance <= 1.4f + baseStats.range)
            {
                //2 because some melle units have a range of 0.5 
                if (baseStats.rangedAttack != 0 && distance >= 2f)
                {
                    if (WillHit(baseStats.precission))
                    {
                        aggroUnit.TakeDamage(baseStats.rangedAttack, baseStats.rangedArmorPiercing);
                    }
                    return attackCooldown + attackCooldown * baseStats.shootingSpeed;
                }

                aggroUnit.TakeDamage(baseStats.meleeAttack, baseStats.meleeArmorPiercing);
                return attackCooldown + attackCooldown * baseStats.shootingSpeed;
            }
            return -1;
        }

        private static bool WillHit(int precission)
        {
            float rNG = Random.Range(0,100);
            if (rNG>precission)
            {
                return false;
            }
            return true;
        }
        
    }
}