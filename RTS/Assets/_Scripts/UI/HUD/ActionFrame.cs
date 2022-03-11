using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NR.RTS.UI.HUD
{

    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;
        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();

        private void Awake()
        {
            instance = this;
        }

        public void SetActionButtons(PlayerActions actions)
        {
            if (actions.units.Length>0)
            {
                foreach (Units.Unit unit in actions.units)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;

                    buttons.Add(btn);
                }
            }

            if (actions.basicBuildings.Length > 0)
            {
                foreach (Buildings.BasicBuilding building in actions.basicBuildings)
                {
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = building.name;

                    buttons.Add(btn);
                }
            }
        }

        public void ClearActions()
        {
            foreach (Button btn in buttons)
            {
                buttons.Remove(btn);
                Destroy(btn);
            }
        }
    }
}