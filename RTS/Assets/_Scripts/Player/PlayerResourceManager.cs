using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.Player
{

    public class PlayerResourceManager : MonoBehaviour
    {
        public static PlayerResourceManager instance = null;
        public Transform goldAmmountTransform;
        public float gold = 0;

        private void Awake()
        {
            instance = this;
            UpdateGoldDisplay();
        }

        public void AddGold(float earnedGold)
        {
            gold += earnedGold;
            UpdateGoldDisplay();
        }

        public void RemoveGold(float spentGold)
        {
            gold -= spentGold;
            UpdateGoldDisplay();
        }

        public void UpdateGoldDisplay()
        {
            goldAmmountTransform.GetComponent<Text>().text = "Gold: " + gold;
        }
    }
}