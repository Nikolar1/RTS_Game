using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NR.RTS.Resources
{
    public class ResourceHandler : MonoBehaviour
    {
        public static ResourceHandler instance;
        public BasicResource gold;

        private void Awake()
        {
            instance = this;
        }

        public BasicResource GetResource(string type)
        {
            BasicResource resource;
            switch (type.ToLower())
            {
                case "gold":
                    resource = gold;
                    break;
                default:
                    Debug.LogError($"Type {type} could not be found");
                    resource = null;
                    break;
            }
            return resource;
        }
    }
}