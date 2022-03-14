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

        public List<Units.Unit> units = new List<Units.Unit>();

        [Space(5)]
        [Header("Units")]
        [Space(15)]
        public List<Buildings.BasicBuilding> basicBuildings = new List<Buildings.BasicBuilding>();
    }
}
