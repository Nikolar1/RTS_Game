using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Units
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public abstract class SharedUnit : MonoBehaviour, IDamageable
    {
        public UnitStats.Base baseStats;
        public float attackCooldown;
        public float currentAttackCooldown;
        protected float lastCheckedTime;
        public GameObject unitDisplay;

        public Image healthBarAmount;

        public float currentHealth = 1;
        protected bool isRepairing = false;

        protected Transform target;
        protected bool hasTarget = false;
        protected IDamageable targetUnit;
        protected VectorDestinationSetter vDS;
        protected const float aggroDistance = 10;
        protected Collider2D[] rangeColliders;
        public AudioSource effectsSource;
        public List<AudioClip> clipQueue;

        protected void SharedUpdate()
        {
            if (!effectsSource.isPlaying && clipQueue.Count > 0)
            {
                PlaySound();
            }
        }

        private void PlaySound()
        {
            effectsSource.clip = clipQueue[0];
            effectsSource.Play();
            clipQueue.Remove(clipQueue[0]);
        }


        protected void Awake()
        {
            vDS = GetComponent<VectorDestinationSetter>();
        }


        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, baseStats.armor, baseStats.defence, currentHealth);
            if (clipQueue.Count <= 2)
            {
                clipQueue.Add(baseStats.damagedSounds[Random.Range(0, baseStats.damagedSounds.Length - 1)]);
            }
        }

        protected void Die()
        {
            Destroy(gameObject);
        }

        protected void Repair()
        {
            if (target == null)
            {
                isRepairing = false;
                vDS.RemoveDestination();
                return;
            }
            float distance = Vector2.Distance(target.position, transform.position);
            if (currentAttackCooldown <= 0 && distance <= Mathf.Max(target.transform.GetComponent<BoxCollider2D>().size.x, target.transform.GetComponent<BoxCollider2D>().size.y) + .1f)
            {
                float temp = target.GetComponent<Buildings.Player.PlayerBuilding>().Repair();
                currentAttackCooldown = attackCooldown;
                clipQueue.Add(baseStats.workingSounds[Random.Range(0, baseStats.workingSounds.Length - 1)]);
                if (temp == -1)
                {
                    target = null;
                    isRepairing = false;
                    vDS.RemoveDestination();
                    return;
                }
            }
        }

        protected void Attack()
        {
            if (target == null)
            {
                clipQueue.Clear();
                hasTarget = false;
                vDS.RemoveDestination();
                return;
            }
            float distance = Vector2.Distance(target.position, transform.position);
            float temp = Combat.Attack(currentAttackCooldown, distance, attackCooldown, targetUnit, baseStats);
            if (temp >= 0)
            {
                if (distance > 2f)
                {
                    clipQueue.Add(baseStats.firingSounds[Random.Range(0, baseStats.firingSounds.Length - 1)]);
                }
                else
                {
                    clipQueue.Add(baseStats.fightingSounds[Random.Range(0, baseStats.fightingSounds.Length - 1)]);
                }
                currentAttackCooldown = temp;
            }
            else if (temp == -2)
            {
                clipQueue.Clear();
                hasTarget = false;
                vDS.RemoveDestination();
                return;
            }
        }

        protected void CheckForEnemyTargets(bool isPlayerUnit)
        {
            lastCheckedTime = Time.time;
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
