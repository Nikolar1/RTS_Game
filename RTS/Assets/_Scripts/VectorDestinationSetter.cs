using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NR.RTS.Units.Player
{
    [RequireComponent(typeof(AIPath))]
    public class VectorDestinationSetter : MonoBehaviour
    {
        private AIPath aIPath;

        private void Awake()
        {
            aIPath = GetComponent<AIPath>(); 
        }

        public void SetDestination(Vector2 destination)
        {
            aIPath.destination = destination;
        }
    }

}