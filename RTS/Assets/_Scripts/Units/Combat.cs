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
        public static float Attack(float currentAttackCooldown, float distance, float range, Player.PlayerUnit aggroUnit, float meleeAttack, int meleeArmorPiercing, float attackCooldown, float shootingSpeed)
        {
            //1.4 is set as the base range because  the distance might be greater than 1.2 when the units meet
            if (currentAttackCooldown <= 0 && distance <= 1.4f + range)
            {
                aggroUnit.TakeDamage(meleeAttack, meleeArmorPiercing);
                return attackCooldown + attackCooldown * shootingSpeed;
            }
            return -1;
        }
    }
}