using UnityEngine;
using UnityEngine.Rendering;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    public class ObjectFade : MonoBehaviour
    {
        public List<Renderer> renderers = new List<Renderer>();

        private bool cameraInRange;

        private CameraCollider cameraCollider;

        private Transform cameraTransform;

        private Collider m_collider;

        private List<Material> materials = new List<Material>();

        private int materialCount;

        readonly int hashFade = Shader.PropertyToID("Fade");

        private float minSqrDistance = 0.01f;
        private float maxSqrDistance = 0.25f;

        private float opacity = 1.0f;

        private ObjectFadeGroup fadeGroup;

        private bool usingFadeGroup;

        private void Awake()
        {
            FindLayer();

            // ============================================

            m_collider = GetComponent<Collider>();

            m_collider.isTrigger = false;

            // ============================================

            Renderer m_renderer = GetComponent<Renderer>();

            if (m_renderer != null && m_renderer.enabled)
            {
                renderers.Add(m_renderer);
            }

            // ============================================

            int listCount = renderers.Count;

            for (int i = 0; i < listCount; i ++)
            {
                int arrayLength = renderers[i].materials.Length;

                for (int j = 0; j < arrayLength; j ++)
                {
                    materials.Add(renderers[i].materials[j]);
                }
            }

            materialCount = materials.Count;

            // ============================================
            
            FadeMaterials(opacity);
        }

        private void OnEnable()
        {
            float minDistance = Camera.main.nearClipPlane;

            minSqrDistance = minDistance * minDistance;

            if (minSqrDistance < 0.01f)
                minSqrDistance = 0.01f;
        }

        private void OnDisable()
        {
            if (cameraCollider != null)
                cameraCollider.objectList.Remove(this);

            SetActive(false);
        }

        private void Update()
        {
            if (cameraInRange && cameraTransform != null)
            {
                Vector3 cameraPosition = cameraTransform.position;

                Vector3 closestPoint = Physics.ClosestPoint(cameraPosition, m_collider, m_collider.transform.position, m_collider.transform.rotation);

                Vector3 difference = cameraPosition - closestPoint;

                // ============================================

                float distanceSquared = difference.sqrMagnitude;

                if (distanceSquared > maxSqrDistance)
                {
                    opacity = 1.0f;
                }

                else
                {
                    opacity = NormalizedValue(distanceSquared, minSqrDistance, maxSqrDistance);
                }

                // ============================================

                if (usingFadeGroup)
                {
                    fadeGroup.SetOpacity(opacity);
                }

                else
                {
                    FadeMaterials(opacity);
                }
            }
        }

        public void FadeMaterials(float value) // called internally and by ObjectFadeGroup.cs
        {
            for (int i = 0; i < materialCount; i ++)
            {
                materials[i].SetFloat(hashFade, 1.0f - value);
            }
        }

        private float NormalizedValue(float value, float min, float max)
        {
            float normalizedValue = (value - min) / (max - min);

            return normalizedValue;
        }

        public void SetActive(bool value)
        {
            cameraInRange = value;

            if (!value)
            {
                opacity = 1.0f;

                FadeMaterials(opacity);
            }
        }

        public void SetCameraCollider(CameraCollider cameraCollider)
        {
            this.cameraCollider = cameraCollider;

            cameraTransform = cameraCollider.transform;
        }

        private void FindLayer()
        {
            int objectFadeLayer = LayerMask.NameToLayer("Object Fade");

            if (objectFadeLayer == -1)
                objectFadeLayer = 0;

            gameObject.layer = objectFadeLayer;
        }

        public void PreserveShadows()
        {
            int listCount = renderers.Count;

            if (listCount > 0)
            {
                for (int i = 0; i < listCount; i ++)
                {
                    CreateShadow(renderers[i]);
                }
            }

            else
            {
                CreateShadow(gameObject.GetComponent<Renderer>());
            }
        }

        private void CreateShadow(Renderer renderer)
        {
            #if UNITY_EDITOR

                GameObject newObject = Instantiate(renderer.gameObject, renderer.transform);

            newObject.name = "Shadow";

            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localEulerAngles = Vector3.zero;

            newObject.transform.localScale = Vector3.one;

            // ============================================

            int childCount = newObject.transform.childCount;

            for (int i = 0; i < childCount; i ++)
            {
                GameObject childObject = newObject.transform.GetChild(0).gameObject;

                DestroyImmediate(childObject);
            }

            // ============================================

            Renderer shadowRenderer = newObject.transform.GetComponent<Renderer>();

            shadowRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;

            renderer.shadowCastingMode = ShadowCastingMode.Off;

            // ============================================

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Material defaultMaterial = cube.GetComponent<Renderer>().sharedMaterial;

            Material[] sharedMaterials = shadowRenderer.sharedMaterials;

            int materialCount = sharedMaterials.Length;

            for (int i = 0; i < materialCount; i ++)
            {
                sharedMaterials[i] = defaultMaterial;
            }

            shadowRenderer.sharedMaterials = sharedMaterials;

            DestroyImmediate(cube);

            // ============================================

            ObjectFade objectFade = newObject.GetComponent<ObjectFade>();

            if (objectFade != null)
                DestroyImmediate(objectFade);

            Collider collider = newObject.GetComponent<Collider>();

            if (collider != null)
                DestroyImmediate(collider);

            // ============================================

            int shadowCount = 0;

            childCount = renderer.transform.childCount;

            for (int i = 0; i < childCount; i ++)
            {
                string childName = renderer.transform.GetChild(i).gameObject.name;

                if (childName == "Shadow")
                    shadowCount ++;
            }

            if (shadowCount > 1)
            {
                string logA = "There are two or more child objects named <color=#80E7FF>Shadow</color> in <color=#80E7FF>" + renderer.gameObject.name + "</color>";
                string logB = ". It is advisable to remove duplicate shadows to save rendering performance.";

                Debug.Log(logA + logB);
            }

            #endif
        }

        public ObjectFadeGroup GetFadeGroup()
        {
            return fadeGroup;
        }

        public void SetFadeGroup(ObjectFadeGroup fadeGroup) // called by ObjectFadeGroup.cs
        {
            this.fadeGroup = fadeGroup;

            usingFadeGroup = true;
        }

        public bool IsUsingFadeGroup() // called by CameraCollider.cs
        {
            return usingFadeGroup;
        }
    }
}









