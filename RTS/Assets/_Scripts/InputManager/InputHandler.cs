using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NR.RTS.Units.Player;
using UnityEngine.EventSystems;

namespace NR.RTS.InputManager {
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        private RaycastHit2D hit;

        public List<Transform> selectedUnits = new List<Transform>();
        public Transform selectedBuilding = null;

        public LayerMask interactableLayer = new LayerMask();

        private bool isDragging = false;

        private Vector2 mousePosition;

        private void Awake()
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

        public RaycastHit2D CheckForHit()
        {
            mousePosition = Input.mousePosition;
            //Get input position
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return Physics2D.Raycast(worldPoint, Vector2.zero, 100);
        }

        public void HandleCameraMovment()
        {
            CameraController.instance.Move(HandleCameraPan());
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                CameraController.instance.Zoom(scroll);
            }
        }

        private Vector3 HandleCameraPan()
        {
            Vector3 direction = Vector3.zero;
            //Screen height and width is calculated from the bottom left corner 
            if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - CameraController.instance.panBorderThickness)
            {
                direction.y = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= CameraController.instance.panBorderThickness)
            {
                direction.y = -1;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= CameraController.instance.panBorderThickness)
            {
                direction.x = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - CameraController.instance.panBorderThickness)
            {
                direction.x = 1;
            }
            return direction;
        }

        public void HandleUnitMovment()
        {

            if (Input.GetMouseButtonDown(0))
            {
                //Check if the pointer is over the UI 
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                //Check if something was hit
                RaycastHit2D hit = CheckForHit();
                if (hit.collider != null)
                {
                    if (addedUnit(hit.transform, Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
                    {

                    }
                    else if (addedBuilding(hit.transform))
                    {

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
                            
                            addedUnit(unit, true);
                        }
                    }
                }
                isDragging = false;
            }


            //Moving selected units to right click position 
            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                //Check if something was hit
                RaycastHit2D hit = CheckForHit();
               
                if (hit.collider != null)
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: //Player object

                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(hit.transform);
                            }
                            break;
                        case 9: //Enemy object
                            foreach (Transform unit in selectedUnits)
                            {

                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(hit.transform, false);
                            }
                            break;
                        case 10:
                            
                            break;
                        default:
                            foreach(Transform unit in selectedUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.MoveUnit(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                            }
                            break;
                    }
                }
                else
                {
                    foreach (Transform unit in selectedUnits)
                    {
                        PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();

                        pU.MoveUnit(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && HaveSelectedBuilding())
            {
                selectedBuilding.gameObject.GetComponent<Interactable.IBuilding>().SetRallyPoint();
            }
        }

        private void DeselectUnits()
        {
            if (selectedBuilding)
            {
                selectedBuilding.gameObject.GetComponent<Interactable.IBuilding>().OnInteractExit();
                selectedBuilding = null;
            }
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                //Sets an object called Highlight on all seleted units to inactive
                //selectedUnits[i].Find("Highlight").gameObject.SetActive(false);
                selectedUnits[i].gameObject.GetComponent<Interactable.IUnit>().OnInteractExit();
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

        private bool HaveSelectedBuilding()
        {
            if (selectedBuilding != null)
            {
                return true;
            }
            return false;
        }

        private Interactable.IUnit addedUnit(Transform tf, bool canMultiselect = false)
        {
            Interactable.IUnit iUnit = tf.GetComponent<Interactable.IUnit>();
            if (iUnit)
            {
                if (!canMultiselect)
                {
                    DeselectUnits();
                }
                selectedUnits.Add(iUnit.gameObject.transform);
                iUnit.OnInteractEnter();
                return iUnit;
            }
            else
            {
                return null;
            }
        }

        private Interactable.IBuilding addedBuilding(Transform tf)
        {
            Interactable.IBuilding iBuilding = tf.GetComponent<Interactable.IBuilding>();
            if (iBuilding)
            {
                DeselectUnits();
                selectedBuilding = iBuilding.gameObject.transform;
                iBuilding.OnInteractEnter();
                return iBuilding;
            }
            else
            {
                return null;
            }
        }
    }
}

