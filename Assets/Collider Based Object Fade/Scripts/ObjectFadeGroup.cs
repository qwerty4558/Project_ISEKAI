using UnityEngine;
using UnityEngine.Rendering;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    public class ObjectFadeGroup : MonoBehaviour
    {
        public List<ObjectFade> objects = new List<ObjectFade>();

        private List<ObjectFade> activeObjects = new List<ObjectFade>();

        private int objectCount;

        private float opacity = 1.0f;

        private bool cameraInRange;

        private void Awake()
        {
            int listCount = objects.Count;

            for (int i = 0; i < listCount; i ++)
            {
                objects[i].SetFadeGroup(this);
            }

            objectCount = listCount;

            // ============================================

            activeObjects.Clear();
        }

        private void Update()
        {
            if (cameraInRange)
            {
                FadeObjects();

                opacity = 1.0f;
            }
        }

        private void FadeObjects()
        {
            for (int i = 0; i < objectCount; i ++)
            {
                objects[i].FadeMaterials(opacity);
            }
        }

        public void SetOpacity(float value) // called by ObjectFade.cs
        {
            if (value < opacity)
            {
                opacity = value;
            }
        }

        public void SetActive(bool value, ObjectFade objectFade)
        {
            if (value)
            {
                cameraInRange = true;

                if (!activeObjects.Contains(objectFade))
                {
                    activeObjects.Add(objectFade);
                }
            }

            else
            {
                activeObjects.Remove(objectFade);

                if (activeObjects.Count == 0)
                {
                    cameraInRange = false;

                    opacity = 1.0f;

                    for (int i = 0; i < objectCount; i ++)
                    {
                        objects[i].FadeMaterials(opacity);
                    }
                }
            }
        }
    }
}









