using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.CampaingMap
{
    [CreateAssetMenu(fileName = "New Interactive Mission", menuName = "New Interactive Mission")]
    public class InteractiveMission : ScriptableObject
    {
        public Sprite missionBackground;
        [Space(15)]
        [Header("Event Text")]
        public string header; 
        public string eventExplanation;

        [Space(15)]
        [Header("Event Options")]
        [Space(40)]
        public string optionOne;
        public string optionTwo;
        public string optionThree;

        [Space(15)]
        [Header("Event Conclusion")]
        [Space(40)]
        public string sharedText;
        public string optionOneConsequence;
        public string optionTwoConsequence;
        public string optionThreeConsequence;

        [Space(15)]
        [Header("Option One Consequences")]
        [Space(40)]
        public int optionOneGold;
        public int optionOneTierOnePopulation;
        public int optionOneTierTwoPopulation;
        public float optionOneTimeElapsed;

        [Space(15)]
        [Header("Option Two Consequences")]
        [Space(40)]
        public int optionTwoGold;
        public int optionTwoTierOnePopulation;
        public int optionTwoTierTwoPopulation;
        public float optionTwoTimeElapsed;

        [Space(15)]
        [Header("Option Three Consequences")]
        [Space(40)]
        public int optionThreeGold;
        public int optionThreeTierOnePopulation;
        public int optionThreeTierTwoPopulation;
        public float optionThreeTimeElapsed;
    }
}