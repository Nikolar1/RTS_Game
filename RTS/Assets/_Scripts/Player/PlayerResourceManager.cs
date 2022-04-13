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
        public Transform tierOnePopulationAmmountTransform;
        public Transform tierTwoPopulationAmmountTransform;

        private void Awake()
        {
            instance = this;

            UpdateGoldDisplay();
            UpdateTierOnePopulationDisplay();
            UpdateTierTwoPopulationDisplay();
        }

        //Gold functions--------------------------------------------------------------------------------
        public void AddGold(float earnedGold)
        {
            CampaingMap.ResourceController.instance.gold += earnedGold;
            UpdateGoldDisplay();
        }

        public void RemoveGold(float spentGold)
        {
            CampaingMap.ResourceController.instance.gold -= spentGold;
            UpdateGoldDisplay();
        }

        public void UpdateGoldDisplay()
        {
            goldAmmountTransform.GetComponent<Text>().text = "Gold: " + CampaingMap.ResourceController.instance.gold;
        }

        public float GetGoldAmmount()
        {
            return CampaingMap.ResourceController.instance.gold;
        }

        //Population functions--------------------------------------------------------------------------------

        public void AddPopulation(Units.UnitStats.Base.UnitPopulationType tier)
        {
            if (tier == Units.UnitStats.Base.UnitPopulationType.TierOne )
            {
                AddTierOnePopulation();
            }
            else
            {
                AddTierTwoPopulation();
            }
        }

        public void RemovePopulation(Units.UnitStats.Base.UnitPopulationType tier)
        {
            if (tier == Units.UnitStats.Base.UnitPopulationType.TierOne)
            {
                RemoveTierOnePopulation();
            }
            else
            {
                RemoveTierTwoPopulation();
            }
        }
        public bool CheckPopulation(Units.UnitStats.Base.UnitPopulationType tier)
        {
            if (tier == Units.UnitStats.Base.UnitPopulationType.TierOne && GetTierOnePopulationAmmount()>0)
            {
                return true;
            }
            if (tier == Units.UnitStats.Base.UnitPopulationType.TierTwo && GetTierTwoPopulationAmmount() > 0)
            {
                return true;
            }
            return false;
        }


        //Tier one population functions--------------------------------------------------------------------------------
        private void AddTierOnePopulation()
        {
            CampaingMap.ResourceController.instance.tierOnePopulation++;
            UpdateTierOnePopulationDisplay();
        }

        private void RemoveTierOnePopulation()
        {
            CampaingMap.ResourceController.instance.tierOnePopulation--;
            UpdateTierOnePopulationDisplay();
        }

        private void UpdateTierOnePopulationDisplay()
        {
            tierOnePopulationAmmountTransform.GetComponent<Text>().text = "T1P: " + CampaingMap.ResourceController.instance.tierOnePopulation;
        }

        private float GetTierOnePopulationAmmount()
        {
            return CampaingMap.ResourceController.instance.tierOnePopulation;
        }

        //Tier one population functions--------------------------------------------------------------------------------
        private void AddTierTwoPopulation()
        {
            CampaingMap.ResourceController.instance.tierTwoPopulation++;
            UpdateTierTwoPopulationDisplay();
        }

        private void RemoveTierTwoPopulation()
        {
            CampaingMap.ResourceController.instance.tierTwoPopulation--;
            UpdateTierTwoPopulationDisplay();
        }

        private void UpdateTierTwoPopulationDisplay()
        {
            tierTwoPopulationAmmountTransform.GetComponent<Text>().text = "T2P: " + CampaingMap.ResourceController.instance.tierTwoPopulation;
        }

        private float GetTierTwoPopulationAmmount()
        {
            return CampaingMap.ResourceController.instance.tierTwoPopulation;
        }
    }
}