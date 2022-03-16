using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Units
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public abstract class SharedUnit : MonoBehaviour, Damageable
    {
        public UnitStats.Base baseStats;
        public float attackCooldown;
        public float currentAttackCooldown;

        public GameObject unitDisplay;

        public Image healthBarAmount;

        public float currentHealth = 1;

        protected Transform target;
        protected bool hasTarget = false;
        protected Damageable targetUnit;
        protected VectorDestinationSetter vDS;
        protected const float aggroDistance = 10;
        protected Collider2D[] rangeColliders;



        protected void Awake()
        {
            vDS = GetComponent<VectorDestinationSetter>();
        }


        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, baseStats.armor, baseStats.defence, currentHealth);
        }

        protected void Die()
        {
            Destroy(gameObject);
        }

        protected void Attack()
        {
            if (target == null)
            {
                hasTarget = false;
                vDS.RemoveDestination();
                return;
            }
            float distance = Vector2.Distance(target.position, transform.position);
            float temp = Combat.Attack(currentAttackCooldown, distance, attackCooldown, targetUnit, baseStats);
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

        protected void CheckForEnemyTargets(bool isPlayerUnit)
        {
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, aggroDistance);
            //Distance cant be greater than 10 because thats the radius of the collision checking circle so 50 just to be sure
            float clossestDistance = 50;
            int clossestCollision = 0;
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer == UnitHandler.instance.pUnitLayer && !isPlayerUnit)
                {
                    float newDistance = Vector2.Distance(transform.position, rangeColliders[i].gameObject.transform.position);
                    if (clossestDistance > newDistance)
                    {
                        clossestDistance = newDistance;
                        clossestCollision = i;
                    }
                    hasTarget = true;
                }
                else if (rangeColliders[i].gameObject.layer == UnitHandler.instance.eUnitLayer && isPlayerUnit)
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
            if (hasTarget && !isPlayerUnit)
            {
                target = rangeColliders[clossestCollision].gameObject.transform;
                targetUnit = target.gameObject.GetComponent<Player.PlayerUnit>();
                if (targetUnit == null)
                {
                    targetUnit = target.gameObject.GetComponent<Buildings.Player.PlayerBuilding>();
                }
            }
            else if (hasTarget && isPlayerUnit)
            {
                target = rangeColliders[clossestCollision].gameObject.transform;
                targetUnit = target.gameObject.GetComponent<Enemy.EnemyUnit>();
                /*Uncomment once enemy buildings are added
                if (targetUnit == null)
                {
                    targetUnit = target.gameObject.GetComponent<Buildings.Enemy.EnemyBuilding>();
                }*/
            }
        }

        public void SetSpeed()
        {
            vDS.SetSpeed(baseStats.speed);
        }

    }
}
