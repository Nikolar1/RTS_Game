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

        private void Update()
        {
            currentAttackCooldown -= Time.deltaTime;
            if (hasTarget)
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
            targetUnit = target.GetComponent<Enemy.EnemyUnit>();

            //1.2 is the distance needed for the unit to stop just as it collides with the enemy
            vDS.SetDestination(target, 1.2f + baseStats.range);
        }

        new private void Die()
        {
            InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            base.Die();
        }
    }
}