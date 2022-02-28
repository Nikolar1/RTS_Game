using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.InputManager {
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        private RaycastHit2D hit; //what we hit with our ray
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HandleUnitMovment()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Create a ray,
                //Ray2D ray = Camera.main.ScreenPointToRay
                //shoot that ray to see if we hit our unit,
                //if we do, then do something with that data

            }
        }
    }
}

