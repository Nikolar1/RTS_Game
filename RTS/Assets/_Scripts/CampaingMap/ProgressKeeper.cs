using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NR.RTS.CampaingMap
{
    public class ProgressKeeper : MonoBehaviour
    {
        public static ProgressKeeper instance;
        public int currentMissionIndex = 0;
        public int currentInteractiveMissionIndex = 0;
        public int currentNodeIndex = 0;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
