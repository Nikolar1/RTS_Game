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
            unitDisplay.SetActive(true);
            if (transform.GetComponent<Buildings.Player.PlayerBuilding>().isBuilt)
            {
                rallyPoint.SetActive(true);
                UI.HUD.ActionFrame.instance.SetActionButtons(actions, transform, rallyPoint);
            }
            
            base.OnInteractEnter();
        }

        public override void OnInteractExit()
        {
            unitDisplay.SetActive(false);
            if (transform.GetComponent<Buildings.Player.PlayerBuilding>().isBuilt)
            {
                UI.HUD.ActionFrame.instance.ClearActions(transform);
                rallyPoint.SetActive(false);
            }
            
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