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
        public Transform target;
        private Damageable targetUnit;
        private bool hasTarget = false;
        private float distance;
        private Collider2D[] rangeColliders;
        private const float aggroDistance = 5;
        public float attackCooldown;
        public float currentAttackCooldown;

        public bool isBuilt = false;



        private void Update()
        {
            if (!isBuilt)
            {
                return;
            }
            currentAttackCooldown -= Time.deltaTime;
            if (!hasTarget)
            {
                CheckForEnemyTargets();
            }
            else
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

        private void Die()
        {
            InputManager.InputHandler.instance.selectedBuilding = null;
            Destroy(gameObject);
        }

        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, 0, 0, currentHealth);
        }
        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, aggroDistance);
            //Distance cant be greater than 10 because thats the radius of the collision checking circle so 50 just to be sure
            float clossestDistance = 50;
            int clossestCollision = 0;
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == UnitHandler.instance.eUnitLayer)
                {
                    float newDistance = Vector2.Distance(transform.position, rangeColliders[i].gameObject.transform.position);
                    if (clossestDistance > newDistance)
                    {
                        clossestDistance = newDistance;
                        clossestCollision = i;
                    }
                    hasTarget = true;
                }
            }
            if (hasTarget)
            {
                target = rangeColliders[clossestCollision].gameObject.transform;
                targetUnit = target.gameObject.GetComponent<Units.Enemy.EnemyUnit>();
                /*Uncomment when enemy buildings are added
                if (aggroUnit == null)
                {
                    aggroUnit = aggroTarget.gameObject.GetComponent<Buildings.Player.EnemyBuilding>();
                }*/
            }
        }

        protected void Attack()
        {
            if (target == null)
            {
                hasTarget = false;
                return;
            }
            float distance = Vector2.Distance(target.position, transform.position);
            Units.UnitStats.Base baseStats = new UnitStats.Base();
            baseStats.rangedAttack = this.baseStats.attack;
            baseStats.rangedArmorPiercing = this.baseStats.armorPiercing;
            baseStats.precission = this.baseStats.precission;
            baseStats.range = this.baseStats.range;
            baseStats.shootingSpeed = this.baseStats.shootingSpeed;
            float temp = Combat.Attack(currentAttackCooldown, distance, attackCooldown, targetUnit, baseStats, true);
            if (temp >= 0)
            {
                currentAttackCooldown = temp;
            }
            else if (temp == -2)
            {
                hasTarget = false;
                return;
            }
        }

        public float Repair()
        {
            currentHealth = Mathf.Min(baseStats.health, currentHealth + RTS.Player.PlayerManager.instance.buildSpeed);
            if (currentHealth == baseStats.health)
            {
                if (!isBuilt)
                {
                    isBuilt = true;
                }
                return -1f;
            }
            return 0f;
        }

    }
}