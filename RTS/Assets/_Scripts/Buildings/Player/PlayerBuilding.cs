using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NR.RTS.Units;

namespace NR.RTS.Buildings.Player
{
    public class PlayerBuilding : MonoBehaviour, Damageable
    {
        public BuildingStatTypes.Base baseStats;
        public Image healthBarAmount;
        public float currentHealth = 1;

        private void LateUpdate()
        {
            if (Combat.HandleHealth(healthBarAmount, currentHealth, baseStats.health))
            {
                Die();
            }
        }

        private void Die()
        {
            InputManager.InputHandler.instance.selectedBuilding = null;
            Destroy(gameObject);
        }

        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, 0, 0, currentHealth);
        }
    }
}