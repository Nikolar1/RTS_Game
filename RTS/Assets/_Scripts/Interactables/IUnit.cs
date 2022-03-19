using UnityEngine;

namespace NR.RTS.Interactable
{
    public class IUnit : Interactable
    {
        public UI.HUD.PlayerActions actions;
        private Units.Unit.unitType unitType;
        public override void OnInteractEnter()
        {
            
            if (InputManager.InputHandler.instance.selectedUnits.Count == 1)
            {
                UI.HUD.ActionFrame.instance.SetActionButtons(actions, transform);
                unitType = Units.UnitHandler.instance.GetUnit(transform.parent.name.ToLower()).type;
            }
            else
            {
                foreach (Transform item in InputManager.InputHandler.instance.selectedUnits)
                {
                    if (!unitType.Equals(Units.UnitHandler.instance.GetUnit(transform.parent.name.ToLower()).type))
                    {
                        UI.HUD.ActionFrame.instance.ClearActions();
                    }
                }
            }
            base.OnInteractEnter();

        }

        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions();
            base.OnInteractExit();
        }
    }
}
