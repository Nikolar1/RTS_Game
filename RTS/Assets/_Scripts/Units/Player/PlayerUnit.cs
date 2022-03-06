using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Units.Player
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public class PlayerUnit : MonoBehaviour
    {
        public int cost;
        public int armor;
        public int defence;
        public float health;
        public float speed;
        public float meleeAttack;
        public int meleeArmorPiercing;
        public float rangedAttack;
        public int rangedArmorPiercing;
        public int precission;
        public float range;
        public float shootingSpeed;
        public float attackCooldown;
        public float currentAttackCooldown;

        public GameObject unitDisplay;

        public Image healthBarAmount;

        public float currentHealth = 1;

        private Transform target;
        private bool hasTarget = false;

        private VectorDestinationSetter vDS;



        private void Awake()
        {
            vDS = GetComponent<VectorDestinationSetter>();
        }

        private void Update()
        {
            HandleHealth();
        }

        public void MoveUnit(Vector2 destination)
        {
            vDS.SetDestination(destination);
        }

        public void TakeDamage(float damage, int armorPiercing)
        {
            float totalDamage = (damage - (armor * ((1f * armorPiercing) / 100))) * ((100f - defence) / 100);
            if (totalDamage < 0)
            {
                totalDamage = 0;
            }
            currentHealth -= totalDamage;
        }

        public void MoveUnit(Transform target)
        {
            this.target = target;
            hasTarget = true;
            //1.2 is the distance needed for the unit to stop just as it collides with the enemy
            vDS.SetDestination(target, 1.2f+range);
        }

        private void HandleHealth()
        {
            healthBarAmount.fillAmount = currentHealth / health;
            if (currentHealth<=0)
            {
                Die();
            }
        }

        private void Die()
        {
            InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            Destroy(gameObject);
        }
    }
}