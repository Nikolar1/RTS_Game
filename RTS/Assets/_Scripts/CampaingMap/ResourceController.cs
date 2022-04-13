using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NR.RTS.CampaingMap
{
    public class ResourceController : MonoBehaviour
    {
        public static ResourceController instance;
        public float gold = 0;
        public int tierOnePopulation = 10;
        public int tierTwoPopulation = 30;

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
