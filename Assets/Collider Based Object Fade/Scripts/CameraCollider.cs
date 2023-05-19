using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class CameraCollider : MonoBehaviour
    {
        public List<ObjectFade> objectList;

        private int objectFadeLayer;

        private Rigidbody m_rigidbody;

        private SphereCollider m_collider;

        private void Awake()
        {
            FindLayer();

            m_rigidbody = GetComponent<Rigidbody>();

            m_collider = GetComponent<SphereCollider>();
        }

        private void OnEnable()
        {
            ResetValues();

            gameObject.layer = objectFadeLayer;

            Shader.SetGlobalFloat("_UseDistanceFade", 1.0f);
        }

        private void OnDisable()
        {
            ClearObjectList();

            Shader.SetGlobalFloat("_UseDistanceFade", 0f);
        }

        private void ResetValues()
        {
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;

            transform.localScale = Vector3.one;

            // ============================================

            m_rigidbody.constraints = RigidbodyConstraints.None;

            m_rigidbody.constraints = m_rigidbody.constraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX;
            m_rigidbody.constraints = m_rigidbody.constraints | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
            m_rigidbody.constraints = m_rigidbody.constraints | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

            m_rigidbody.useGravity = false;
            m_rigidbody.isKinematic = true;

            // ============================================

            m_collider.isTrigger = true;

            m_collider.center = Vector3.zero;
            m_collider.radius = 0.5f;
        }

        private void ClearObjectList()
        {
            int listCount = objectList.Count;

            for (int i = 0; i < listCount; i ++)
            {
                objectList[i].SetActive(false);
            }

            objectList.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == objectFadeLayer)
            {
                ObjectFade objectFade = other.GetComponent<ObjectFade>();

                if (objectFade != null && !objectList.Contains(objectFade))
                {
                    objectList.Add(objectFade);

                    objectFade.SetCameraCollider(this);

                    objectFade.SetActive(true);

                    // ============================================

                    if (objectFade.IsUsingFadeGroup())
                    {
                        objectFade.GetFadeGroup().SetActive(true, objectFade);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == objectFadeLayer)
            {
                ObjectFade objectFade = other.GetComponent<ObjectFade>();

                if (objectFade != null)
                {
                    objectList.Remove(objectFade);

                    objectFade.SetActive(false);

                    // ============================================

                    if (objectFade.IsUsingFadeGroup())
                    {
                        objectFade.GetFadeGroup().SetActive(false, objectFade);
                    }
                }
            }
        }

        private void FindLayer()
        {
            objectFadeLayer = LayerMask.NameToLayer("Object Fade");

            if (objectFadeLayer == -1)
                objectFadeLayer = 0;

            gameObject.layer = objectFadeLayer;
        }
    }
}









