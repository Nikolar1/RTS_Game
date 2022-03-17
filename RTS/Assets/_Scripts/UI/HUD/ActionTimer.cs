using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.UI.HUD
{
    [RequireComponent(typeof(Buildings.Player.BuildingBuildQueue))]
    public class ActionTimer : MonoBehaviour
    {
        public float spawnTimer;
        public float elapsedTime;
        public Buildings.Player.BuildingBuildQueue buildQueue;
        public bool timerStarted = false;
        public bool timerPaused = false;

        private void Awake()
        {
            buildQueue = GetComponent<Buildings.Player.BuildingBuildQueue>();
        }

        public void StartTimer(float spawnTimer)
        {
            this.spawnTimer = spawnTimer;
            elapsedTime = 0;
            timerStarted = true;
            timerPaused = false;
    }

        public void StopTimer()
        {
            timerPaused = true;
        }

        public void ContinueTimer()
        {
            timerPaused = false;
        }

        public void Update()
        {
            if (timerStarted && !timerPaused)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= spawnTimer)
                {
                    timerStarted = false;
                    buildQueue.SpawnObject();
                }
            }
            
        }


        public IEnumerator SpawnQueueTimer()
        {
            if (ActionFrame.instance.buildQueue.spawnQueue.Count > 0)
            {
                Debug.Log($"Waiting for {ActionFrame.instance.buildQueue.spawnQueue[0]}");

                yield return new WaitForSeconds(ActionFrame.instance.buildQueue.spawnQueue[0]);

                buildQueue.SpawnObject();

               

                if (ActionFrame.instance.buildQueue.spawnQueue.Count > 0)
                {
                    StartCoroutine(SpawnQueueTimer());
                }
            }
        }
    }
}