using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NR.RTS.CampaingMap
{
    public class MissionStarter : MonoBehaviour
    {
        public bool[] nodes;
        public string[] missions;
        public InteractiveMission[] interactiveMissions;
        public GameObject interactiveMissionPanel;
        public GameObject interactiveMissionHeader;
        public GameObject interactiveMissionText;
        public GameObject interactiveMissionOptionOne;
        public GameObject interactiveMissionOptionTwo;
        public GameObject interactiveMissionOptionThree;
        public GameObject interactiveMissionConsequenceText;
        public GameObject backButton;

        public void SelectMission(int index)
        {
            //This check will be changed later
            if(index != ProgressKeeper.instance.currentNodeIndex + 1)
            {
                return;
            }


            //Checks if the mission is interactive or not
            if (nodes[index])
            {
                StartInteractiveMision();
            }
            else
            {
                ProgressKeeper.instance.currentMissionIndex++;
                StartMission(ProgressKeeper.instance.currentMissionIndex-1);
            }
        }

        public void OptionSelected(int index)
        {
            interactiveMissionOptionOne.transform.parent.gameObject.SetActive(false);
            interactiveMissionOptionTwo.transform.parent.gameObject.SetActive(false);
            interactiveMissionOptionThree.transform.parent.gameObject.SetActive(false);
            backButton.SetActive(true);
            interactiveMissionConsequenceText.SetActive(true);
            if (index == 0)
            {
                interactiveMissionText.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].sharedText + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneConsequence;
                interactiveMissionConsequenceText.GetComponent<Text>().text = "Gold change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneGold + "\nTier one population change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneTierOnePopulation + "\nTier two population change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneTierTwoPopulation;
                ResourceController.instance.gold += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneGold;
                ResourceController.instance.tierTwoPopulation += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneTierTwoPopulation;
                ResourceController.instance.tierOnePopulation += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOneTierOnePopulation;

            }
            else if (index == 1)
            {
                interactiveMissionText.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].sharedText + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoConsequence;
                interactiveMissionConsequenceText.GetComponent<Text>().text = "Gold change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoGold + "\nTier one population change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoTierOnePopulation + "\nTier two population change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoTierTwoPopulation;
                ResourceController.instance.gold += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoGold;
                ResourceController.instance.tierTwoPopulation += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoTierTwoPopulation;
                ResourceController.instance.tierOnePopulation += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwoTierOnePopulation;
            }
            else
            {
                interactiveMissionText.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].sharedText + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeConsequence;
                interactiveMissionConsequenceText.GetComponent<Text>().text = "Gold change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeGold + "\nTier one population change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeTierOnePopulation + "\nTier two population change: " + interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeTierTwoPopulation;
                ResourceController.instance.gold += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeGold;
                ResourceController.instance.tierTwoPopulation += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeTierTwoPopulation;
                ResourceController.instance.tierOnePopulation += interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThreeTierOnePopulation;
            }
        }

        public void ExitInteractiveMission()
        {
            ProgressKeeper.instance.currentInteractiveMissionIndex++;
            interactiveMissionPanel.SetActive(false);
            backButton.SetActive(false);
            ProgressKeeper.instance.currentNodeIndex++;
        }

        private void StartMission(int index)
        {
            SceneManager.LoadScene(missions[index]);
        }

        private void StartInteractiveMision()
        {
            interactiveMissionPanel.SetActive(true);
            backButton.SetActive(false);
            interactiveMissionConsequenceText.SetActive(false);
            interactiveMissionOptionOne.transform.parent.gameObject.SetActive(true);
            interactiveMissionOptionTwo.transform.parent.gameObject.SetActive(true);
            interactiveMissionOptionThree.transform.parent.gameObject.SetActive(true);
            Image panelImage = interactiveMissionPanel.GetComponent<Image>();
            panelImage.sprite = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].missionBackground;
            interactiveMissionHeader.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].header;
            interactiveMissionText.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].eventExplanation;
            interactiveMissionOptionOne.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionOne;
            interactiveMissionOptionTwo.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionTwo;
            interactiveMissionOptionThree.GetComponent<Text>().text = interactiveMissions[ProgressKeeper.instance.currentInteractiveMissionIndex].optionThree;
        }

        public void ReturnToMainMenu()
        {
            Destroy(ProgressKeeper.instance.transform.gameObject);
            Destroy(ResourceController.instance.transform.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
