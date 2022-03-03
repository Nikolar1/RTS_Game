using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using System;

namespace NR.RTS.Units.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]

    public class PlayerUnit : MonoBehaviour
    {
        private Vector2 target;

        public float speed = 200f;
        public float nextWaypointDistance = 3f;
        private Path path;
        private int currentWaypoint = 0;
        private bool reachedEndOfPath = false;
        private Seeker seeker;
        private Rigidbody2D rb;

        private void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            InvokeRepeating("UpdatePath", 0f, .5f);
            target = rb.position;
        }

        private void FixedUpdate()
        {
            if(path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                if(Vector2.Distance(rb.position, target) < 0.1)
                {
                    rb.velocity = Vector2.zero;
                }
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
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

            if (seeker.IsDone() && (Vector2.Distance(rb.position, target) > 1))
            {
                seeker.StartPath(rb.position, target, OnPathComplete);
            }
        }

        public void MoveUnit(Vector2 _destination)
        {
            target = _destination;
        }

    }

}

