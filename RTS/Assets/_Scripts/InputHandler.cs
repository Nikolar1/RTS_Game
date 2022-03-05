using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NR.RTS.Units.Player;

namespace NR.RTS.InputManager {
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        private RaycastHit2D hit;

        private List<Transform> selectedUnits = new List<Transform>();

        private bool isDragging = false;

        private Vector2 mousePosition;
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePosition, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0, 0, 255, (float)0.25));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.grey);
            }
        }

        private RaycastHit2D checkForHit()
        {
            mousePosition = Input.mousePosition;
            //Get input position
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return Physics2D.Raycast(worldPoint, Vector2.zero);
        }

        public void HandleUnitMovment()
        {

            if (Input.GetMouseButtonDown(0))
            {
                //Check if something was hit
                RaycastHit2D hit = checkForHit();
                if (hit.collider != null)
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: // Layer 8 has been designated as the player unit layer
                            //If neither of the Control keys is held down sends false for canMultiselect
                            SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
                            isDragging = true;
                            break;
                        default:
                            //Delete selection if no unit is pressed and neither of the Control keys is held down
                            isDragging = true;
                            DeselectUnits();
                            break;
                    }
                }
                else if(!Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    //Set isDragging to true to be used in the drag selection
                    isDragging = true;
                    //Delete selection if no unit is pressed and neither of the Control keys is held down
                    DeselectUnits();
                }
                else
                {
                    isDragging = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach(Transform unit in child)
                    {
                        if (isWithinSelectionBounds(unit))
                        {
                            
                            SelectUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                //Check if something was hit
                RaycastHit2D hit = checkForHit();
                if (hit.collider != null)
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: //Player unit
                            
                            break;
                        case 9: //Enemy unit

                            break;
                        default:
                            foreach(Transform unit in selectedUnits)
                            {
                                Debug.Log("caooo3");
                                VectorDestinationSetter pU = unit.gameObject.GetComponent<VectorDestinationSetter>();

                                pU.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                            }
                            break;
                    }
                }
                else
                {
                    foreach (Transform unit in selectedUnits)
                    {
                        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        VectorDestinationSetter pU = unit.gameObject.GetComponent<VectorDestinationSetter>();

                        pU.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
            }
        }

        private void SelectUnit(Transform unit, bool canMultiselect = false)
        {
            if (!canMultiselect)
            {
                DeselectUnits();
            }
            selectedUnits.Add(unit);
            //Sets an object called Highlight on the unit
            unit.Find("Highlight").gameObject.SetActive(true);
        }

        private void DeselectUnits()
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                //Sets an object called Highlight on all seleted units to inactive
                selectedUnits[i].Find("Highlight").gameObject.SetActive(false);
            }
            selectedUnits.Clear();
        }

        private bool isWithinSelectionBounds(Transform tf)
        {
            if (!isDragging)
            {
                return false;
            }
            Camera camera = Camera.main;
            Bounds viewPortBounds = MultiSelect.GetViewPortBounds(camera, mousePosition, Input.mousePosition);
            Vector3 unit = camera.WorldToViewportPoint(tf.position);
            unit.z = 0;
            return viewPortBounds.Contains(unit);
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}

