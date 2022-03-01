using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.InputManager
{
    public static class MultiSelect
    {
        private static Texture2D _whiteTexture;

        public static Texture2D WhiteTexture
        {
            get
            {
                if(_whiteTexture == null)
                {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }
                return _whiteTexture;
            }
        }

        public static void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
        }

        public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
        {
            //Top
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            //Bottom
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
            //Left
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            //Right
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        }

        public static Rect GetScreenRect(Vector2 screenPos1, Vector2 screenPos2)
        {
            //go from bottom right to the top left
            screenPos1.y = Screen.height - screenPos1.y;
            screenPos2.y = Screen.height - screenPos2.y;

            //corners
            Vector2 bottomRight = Vector2.Max(screenPos1, screenPos2);
            Vector2 topLeft = Vector2.Min(screenPos1, screenPos2);

            //create the rectangle
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        public static Bounds GetViewPortBounds(Camera camera, Vector2 screenPos1, Vector2 screenPos2)
        {
            Vector3 pos1 = camera.ScreenToViewportPoint(screenPos1);
            Vector3 pos2 = camera.ScreenToViewportPoint(screenPos2);
            pos1.z = 0;
            pos2.z = 0;
            Vector3 min = Vector2.Min(pos1, pos2);
            Vector3 max = Vector2.Max(pos1, pos2);
            Debug.Log(min + " " + max);
            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }

    }

}
