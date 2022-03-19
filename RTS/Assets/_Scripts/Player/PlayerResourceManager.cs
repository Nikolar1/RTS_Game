using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Player
{

    public class PlayerResourceManager : MonoBehaviour
    {
        public static PlayerResourceManager instance = null;
        public float gold = 0;

        private void Awake()
        {
            instance = this;
        }
    }
}