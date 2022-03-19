using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Units.Player
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public class PlayerUnit : SharedUnit
    {
        private bool isMining = false;
        private void Update()
        {
            currentAttackCooldown -= Time.deltaTime;
            if (isRepairing)
            {
                Repair();
            }
            else if (isMining)
            {
                Work();
            }
            else if (hasTarget)
            {
                Attack();
            }
            else
            {
                CheckForEnemyTargets(true);
            }
        }

        protected void LateUpdate()
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

        public void MoveUnit(Transform target, bool isPlayerOwnedTarget = true, bool isResource = false)
        {

            if (isPlayerOwnedTarget)
            {
                //if the target is player owned unit will follow and stop just as it collides
                if (baseStats.canRepair)
                {
                    isRepairing = true;
                    this.target = target;
                }

                vDS.SetDestination(target, 1.2f);
                return;
            }
            else if (isResource)
            {
                isMining = true;
                this.target = target;
                vDS.SetDestination(target, 1.2f);
                return;
            }
            this.target = target;
            hasTarget = true;
            targetUnit = target.GetComponent<Enemy.EnemyUnit>();

            //1.2 is the distance needed for the unit to stop just as it collides with the enemy
            vDS.SetDestination(target, 1.2f + baseStats.range);
        }

        public void Work()
        {
            if (target == null)
            {
                isMining = false;
                vDS.RemoveDestination();
                return;
            }
            float distance = Vector2.Distance(target.position, transform.position);
            if (currentAttackCooldown <= 0 && distance <= 1.4f)
            {
                float temp = target.GetComponent<Resources.Resource>().Mine();
                RTS.Player.PlayerResourceManager.instance.gold += temp;
                if (temp < 25)
                {
                    target = null;
                    isMining = false;
                    vDS.RemoveDestination();
                    return;
                }
            }
        }

        new private void Die()
        {
            InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            base.Die();
        }
    }
}