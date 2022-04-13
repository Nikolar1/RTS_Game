using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NR.RTS.MainMenu
{

    public class MainMenu : MonoBehaviour
    {
        public void ExitButton()
        {
            Application.Quit();
            Debug.Log("Game closed");
        }

        public void StartCampaing()
        {
            SceneManager.LoadScene("CampaingMap");
        }
    }
}