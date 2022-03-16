using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace NR.RTS.Units.Enemy
{
    [RequireComponent(typeof(VectorDestinationSetter))]
    public class EnemyUnit : SharedUnit
    {
        //Distance at which the enemy will start attacking
        private float distance;

        private void Update()
        {
            currentAttackCooldown -= Time.deltaTime;
            if (!hasTarget)
            {
                CheckForEnemyTargets(false);
            }
            else
            {
                Attack();
                MoveToAggroTarget();
            }
        }

        protected void LateUpdate()
        {
            if (Combat.HandleHealth(healthBarAmount, currentHealth, baseStats.health))
            {
                Die();
            }
        }

        private void MoveToAggroTarget()
        {
            if (target == null)
            {
                hasTarget = false;
                vDS.RemoveDestination();
                return;
            }
            distance = Vector2.Distance(target.position, transform.position);
            if (distance <= aggroDistance + aggroDistance*0.1)
            {
                //1.2 is the distance needed for the unit to stop just as it collides with the enemy
                vDS.SetDestination(target, 1.2f + baseStats.range);

            }
            else
            {
                hasTarget = false;
                vDS.RemoveDestination();
            }
        }
    }
}

