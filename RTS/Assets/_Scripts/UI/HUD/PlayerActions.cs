using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.UI.HUD
{
    [CreateAssetMenu(fileName = "NewPlayerActions", menuName = "PlayerAction")]
    public class PlayerActions : ScriptableObject
    {
        [Space(5)]
        [Header("Units")]

        public Units.Unit[] units = new Units.Unit[0];

        [Space(5)]
        [Header("Units")]
        [Space(15)]
        public Buildings.BasicBuilding[] basicBuildings = new Buildings.BasicBuilding[0];
    }
}
