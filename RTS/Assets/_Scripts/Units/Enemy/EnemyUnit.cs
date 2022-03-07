using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace NR.RTS.Units.Enemy
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public class EnemyUnit : MonoBehaviour
    {
        //Distance at which the enemy will start attacking
        private const float aggroDistance = 11;

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

        private Collider2D[] rangeColliders;

        private Transform aggroTarget;
        private Player.PlayerUnit aggroUnit;
        private bool hasAggro = false;

        private float distance;
        private VectorDestinationSetter vDS;

        public GameObject unitDisplay;

        public Image healthBarAmount;

        public float currentHealth = 1;

        public float attackCooldown;
        public float currentAttackCooldown;

        private void Awake()
        {
            vDS = GetComponent<VectorDestinationSetter>();
        }

        private void Update()
        {
            currentAttackCooldown -= Time.deltaTime;
            if (!hasAggro)
            {
                CheckForEnemyTargets();
            }
            else
            {
                Attack();
                MoveToAggroTarget();
            }
        }

        private void LateUpdate()
        {
            if(Combat.HandleHealth(healthBarAmount, currentHealth, health))
            {
                Die();
            }
        }
        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, aggroDistance);
            //Distance cant be greater than 10 because thats the radius of the collision checking circle so 50 just to be sure
            float clossestDistance = 50;
            int clossestCollision = 0;
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if(rangeColliders[i].gameObject.layer == UnitHandler.instance.pUnitLayer)
                {
                    float newDistance = Vector2.Distance(transform.position, rangeColliders[i].gameObject.transform.position);
                    if (clossestDistance>newDistance)
                    {
                        clossestDistance = newDistance;
                        clossestCollision = i;
                    }
                    hasAggro = true;
                }
            }
            if (hasAggro)
            {
                aggroTarget = rangeColliders[clossestCollision].gameObject.transform;
                aggroUnit = aggroTarget.gameObject.GetComponent<Player.PlayerUnit>();
            }
        }

        private void Attack()
        {
            float temp = Combat.Attack(currentAttackCooldown, distance, range, aggroUnit, meleeAttack, meleeArmorPiercing, attackCooldown, shootingSpeed);
            if (temp >= 0)
            {
                currentAttackCooldown = temp;
            }
        }

        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, armor, defence, currentHealth);
        }

        private void MoveToAggroTarget()
        {
            if (aggroTarget == null)
            {
                hasAggro = false;
                vDS.RemoveDestination();
                return;
            }
            distance = Vector2.Distance(aggroTarget.position, transform.position);
            if (distance <= aggroDistance + aggroDistance*0.1)
            {
                //1.2 is the distance needed for the unit to stop just as it collides with the enemy
                vDS.SetDestination(aggroTarget, 1.2f + range);

            }
            else
            {
                hasAggro = false;
                vDS.RemoveDestination();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

