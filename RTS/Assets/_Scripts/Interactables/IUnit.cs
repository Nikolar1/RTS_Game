using UnityEngine;

namespace NR.RTS.Interactable
{
    public class IUnit : Interactable
    {
        public UI.HUD.PlayerActions actions;
        private Units.Unit.UnitType unitType;
        public AudioSource unitedEffectsSource;
        public override void OnInteractEnter()
        {
            Units.Unit unit = Units.UnitHandler.instance.GetUnit(transform.parent.name.ToLower());
            if (InputManager.InputHandler.instance.selectedUnits.Count == 1)
            {
                UI.HUD.ActionFrame.instance.SetActionButtons(actions, transform);
                unitType = unit.type;
            }
            else
            {
                foreach (Transform item in InputManager.InputHandler.instance.selectedUnits)
                {
                    if (!unitType.Equals(Units.UnitHandler.instance.GetUnit(transform.parent.name.ToLower()).type))
                    {
                        UI.HUD.ActionFrame.instance.ClearActions(transform);
                    }
                }
            }

            unitedEffectsSource = transform.GetComponent<RTS.Units.Player.PlayerUnit>().unitedEffectsSource;
            unitedEffectsSource.clip = unit.baseStats.selectionSounds[Random.Range(0, unit.baseStats.selectionSounds.Length - 1)];
            unitedEffectsSource.Play();

            base.OnInteractEnter();

        }

        public override void OnInteractExit()
        {
            UI.HUD.ActionFrame.instance.ClearActions(transform);
            base.OnInteractExit();
        }
    }
}
