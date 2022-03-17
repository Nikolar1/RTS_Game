using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Interactable
{
    public class IBuilding : Interactable
    {
        public UI.HUD.PlayerActions actions;
        public GameObject rallyPoint = null;
        public GameObject unitDisplay;
        public override void OnInteractEnter()
        {
            rallyPoint.SetActive(true);
            unitDisplay.SetActive(true);
            UI.HUD.ActionFrame.instance.SetActionButtons(actions, rallyPoint, transform);
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            rallyPoint.SetActive(false);
            unitDisplay.SetActive(false);
            base.OnInteractExit();
        }

        public void SetRallyPoint()
        {
            RaycastHit2D hit = InputManager.InputHandler.instance.CheckForHit();
            if (hit.collider == null)
            {
                Vector3 loc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                loc.z = 0;
                rallyPoint.transform.position = loc;
                
            }
        }

    }
}