using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NR.RTS.Units;

namespace NR.RTS.Buildings.Player
{
    public class PlayerBuilding : MonoBehaviour, IDamageable
    {
        public BuildingStatTypes.Base baseStats;
        public Image healthBarAmount;
        public float currentHealth = 1;
        public Transform target;
        private IDamageable targetUnit;
        private bool hasTarget = false;
        private Collider2D[] rangeColliders;
        private const float aggroDistance = 5;
        public float attackCooldown;
        public float currentAttackCooldown;
        public AudioSource effectsSource;
        public List<AudioClip> clipQueue;
        public bool isBuilt = false;



        private void Update()
        {
            if (!isBuilt)
            {
                return;
            }
            if (!effectsSource.isPlaying && clipQueue.Count > 0)
            {
                PlaySound();
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

        private void PlaySound()
        {
            effectsSource.clip = clipQueue[0];
            effectsSource.Play();
            clipQueue.Remove(clipQueue[0]);
        }

        private void Die()
        {
            InputManager.InputHandler.instance.selectedBuilding = null;
            transform.GetComponent<Interactable.IBuilding>().OnInteractExit();
            Destroy(gameObject);
        }

        public void TakeDamage(float damage, int armorPiercing)
        {
            currentHealth = Combat.TakeDamage(damage, armorPiercing, 0, 0, currentHealth);
            if (clipQueue.Count<=2)
            {
                clipQueue.Add(baseStats.damagedSounds[Random.Range(0, baseStats.damagedSounds.Length - 1)]);
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
            UnitStats.Base baseStats = new UnitStats.Base();
            baseStats.rangedAttack = this.baseStats.attack;
            baseStats.rangedArmorPiercing = this.baseStats.armorPiercing;
            baseStats.precission = this.baseStats.precission;
            baseStats.range = this.baseStats.range;
            baseStats.shootingSpeed = this.baseStats.shootingSpeed;
            float temp = Combat.Attack(currentAttackCooldown, distance, attackCooldown, targetUnit, baseStats, true);
            
            if (temp >= 0)
            {
                clipQueue.Add(this.baseStats.firingSounds[Random.Range(0, this.baseStats.firingSounds.Length - 1)]);
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
                    RTS.Player.VoiceAssistant.instance.PlayConstructionComplete();
                }
                return -1f;
            }
            return 0f;
        }

    }
}