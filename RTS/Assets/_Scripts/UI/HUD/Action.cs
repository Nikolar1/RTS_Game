using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.UI.HUD
{
    public class Action : MonoBehaviour
    {
        public void OnClick()
        {
            ActionFrame.instance.StartSpawnTimer(name);
        }
    }
}