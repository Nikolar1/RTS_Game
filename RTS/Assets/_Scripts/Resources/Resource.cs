using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NR.RTS.Resources
{
    public class Resource : MonoBehaviour
    {
        public float ammount;
        public float currentAmmount = 1;
        public Image healthBarAmount;

        private void LateUpdate()
        {
            if (currentAmmount <= 0)
            {
                Depleate();
            }
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
                return currentAmmount;
            }
        }

        public void Depleate()
        {
            Destroy(gameObject);
        }

    }
}
