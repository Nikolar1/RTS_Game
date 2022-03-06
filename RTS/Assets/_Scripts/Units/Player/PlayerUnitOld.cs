using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using System;

//Depreciated script, used for moving the units
//which is now done with scripts from A* Pathfinding project
//and PlayerDestinationSetter script

namespace NR.RTS.Units.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]

    public class PlayerUnitOld : MonoBehaviour
    {
        private Vector2 target;
        private bool hasTarget = false;

        public float speed = 200f;
        public float nextWaypointDistance = 1f;
        private Path path;
        private int currentWaypoint = 0;
        private Seeker seeker;
        private Rigidbody2D rb;

        private void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }


        private void FixedUpdate()
        {

            if(path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                //hasTarget = false;
                if (Vector2.Distance(rb.position, target) <= 0.1)
                {
                    rb.velocity = Vector2.zero;
                }
                return;
            }
            Vector2 waypoint = new Vector2();
            waypoint.x = path.vectorPath[currentWaypoint].x;
            waypoint.y = path.vectorPath[currentWaypoint].y;

            Vector2 direction = (waypoint - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.velocity = force;
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

        }
        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        public void UpdatePath()
        {
            if (hasTarget)
            {
                if (seeker.IsDone() && (Vector2.Distance(rb.position,target)>0.5))
                {
                    seeker.StartPath(rb.position, target, OnPathComplete);
                }
            }
        }

        public void MoveUnit(Vector2 _destination)
        {
             target = _destination;
             hasTarget = true;

        }

    }

}

