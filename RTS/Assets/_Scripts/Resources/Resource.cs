using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NR.RTS.Resources
{
    public class Resource : MonoBehaviour
    {
        public BasicResource.ResourceType type;
        public float ammount;
        public float currentAmmount = 1;
        public Image healthBarAmount;

        private void Update()
        {
            if(Units.Combat.HandleHealth(healthBarAmount, currentAmmount, ammount))
            {
                Depleate();
            }
        }

        private void LateUpdate()
        {
            if (currentAmmount <= 0)
            {
                Depleate();
            }
        }

        public void SetAmmount(float baseAmmount)
        {
            ammount = Mathf.Round(Random.Range(baseAmmount * 0.9f, baseAmmount * 1.1f));
            currentAmmount = ammount;
        }

        public float Mine()
        {
            if (currentAmmount >= 25f)
            {
                currentAmmount -= 25f;
                return 25f;
            }
            else
            {
                float rez = currentAmmount;
                currentAmmount = 0;
                return rez;
            }
        }

        public void Depleate()
        {
            Player.VoiceAssistant.instance.PlayResourceDepleated();
            Destroy(gameObject);
        }



    }
}
