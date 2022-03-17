using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.InputManager
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController instance;
        public float panSpeed = 20f;
        public float panBorderThickness = 10f;
        public float scrollSpeed = 2f;
        public Vector2 panLimit;
        public float minZoom;
        public float maxZoom;
        public Camera cam;

        private void Awake()
        {
            instance = this;
        }

        public void Move(Vector3 direction)
        {
            direction.z = 0;
            Vector3 position = cam.transform.position + direction * (panSpeed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
            position.y = Mathf.Clamp(position.y, -panLimit.y, panLimit.y);
            cam.transform.position = position;
        }

        public void Zoom(float scroll)
        {
            float size = cam.orthographicSize - scroll * scrollSpeed;
            cam.orthographicSize = Mathf.Clamp(size, minZoom, maxZoom);
        }


    }
}
