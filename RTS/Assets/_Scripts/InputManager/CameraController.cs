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
        public Camera camera;

        private void Awake()
        {
            instance = this;
        }

        public void Move(Vector3 direction)
        {
            direction.z = 0;
            Vector3 position = camera.transform.position + direction * (panSpeed * Time.deltaTime);
            position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
            position.y = Mathf.Clamp(position.y, -panLimit.y, panLimit.y);
            camera.transform.position = position;
        }

        public void Zoom(float scroll)
        {
            float size = camera.orthographicSize - scroll * scrollSpeed;
            camera.orthographicSize = Mathf.Clamp(size, minZoom, maxZoom);
        }


    }
}
