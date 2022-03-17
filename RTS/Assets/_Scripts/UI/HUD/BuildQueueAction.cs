using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.UI.HUD
{
    public class BuildQueueAction : MonoBehaviour
    {
        public float spawnTimer;
        public float elapsedTime;
        public bool timerStarted = false;
        public Image indicator;

        public void OnClick()
        {
            ActionFrame.instance.RemoveUnitFromBuildQueue(int.Parse(name));
        }

        public void StartTimer(float elapsedTime = 0)
        {
            this.elapsedTime = spawnTimer-elapsedTime;
            timerStarted = true;
        }

        public void Update()
        {
            if (timerStarted)
            {
                indicator.fillAmount = elapsedTime / spawnTimer;
                elapsedTime -= Time.deltaTime;
            }
        }
    }
}
