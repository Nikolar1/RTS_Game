using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NR.RTS.Units
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(AIDestinationSetter))]
    public class VectorDestinationSetter : MonoBehaviour
    {
        private AIPath aIPath;
        private AIDestinationSetter aIDestination;

        private void Awake()
        {
            aIPath = GetComponent<AIPath>();
            aIDestination = GetComponent<AIDestinationSetter>();
        }

        public void SetDestination(Vector2 destination)
        {
            aIPath.endReachedDistance = 0;
            aIDestination.target = null;
            aIPath.destination = destination;
        }

        public void SetDestination(Transform target, float stopingDistance = 1.2f)
        {
            aIPath.endReachedDistance = stopingDistance;
            aIDestination.target = target;
        }

        public void RemoveDestination()
        {
            aIDestination.target = null;
            aIPath.destination = transform.position;
        }
    }

}