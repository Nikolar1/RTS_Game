using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Units.Player
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public class PlayerUnit : MonoBehaviour, Damageable
    {
        public UnitStats.Base baseStats;
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
            currentAttackCooldown -= Time.deltaTime;
            if (hasTarget)
            {
                Attack();
            }
        }

        private void LateUpdate()
        {
            if (Combat.HandleHealth(healthBarAmount, currentHealth, baseStats.health))
            {
                Die();
            }
        }

        public void MoveUnit(Vector2 destination)
        {
            hasTarget = false;
            vDS.SetDestination(destination);
        }

        public void MoveUnit(Transform target, bool isPlayerOwnedTarget = true)
        {

            if (isPlayerOwnedTarget)
            {
                //if the target is player owned unit will follow and stop just as it collides
                vDS.SetDestination(target, 1.2f);
                return;
            }
            this.target = target;
            hasTarget = true;
            
            //1.2 is the distance needed for the unit to stop just as it collides with the enemy
            vDS.SetDestination(target, 1.2f + baseStats.range);
        }

        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, baseStats.armor, baseStats.defence, currentHealth);
        }



        private void Die()
        {
            InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            Destroy(gameObject);
        }

        private void Attack()
        {
            if (target == null)
            {
                hasTarget = false;
                vDS.RemoveDestination();
                return;
            }
            float distance = Vector2.Distance(target.position, transform.position);
            Enemy.EnemyUnit eu = target.GetComponent<Enemy.EnemyUnit>();
            float temp = Combat.Attack(currentAttackCooldown, distance, attackCooldown, eu, baseStats);
            if (temp >= 0)
            {
                currentAttackCooldown = temp;
            }
            else if (temp == -2)
            {
                hasTarget = false;
                vDS.RemoveDestination();
                return;
            }
        }
    }
}