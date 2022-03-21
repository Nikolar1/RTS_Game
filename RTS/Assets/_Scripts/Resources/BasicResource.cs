using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Resources
{
    [CreateAssetMenu(fileName = "Resource", menuName = "New Resource")]
    public class BasicResource : ScriptableObject
    {
        public enum ResourceType
        {
            Gold
        }

        [Space(15)]
        [Header("Resource Settings")]

        public ResourceType type;
        public new string name;
        public GameObject ResourcePrefab;
        public float amount;
    }
}
