using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Laser : MonoBehaviour
{
    [SerializeField] GameObject obj;
    public Transform target;
    public float rotationSpeed = 1f;
    public float pathDisplayTime = 2f;
    public float laserTime = 5f;
    private LineRenderer lineRenderer;
    private bool isDisplayingPath = false;
    private bool isLaserActive = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        obj.SetActive(false);
    }

    private void Update()
    {
        if (PlayerController.instance != null)
        {
            target = PlayerController.instance.transform;

            Vector3 direction = target.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (!isDisplayingPath)
                StartCoroutine(DisplayPath());
            else if (isDisplayingPath && !isLaserActive)
            {
                obj.SetActive(true);
                isLaserActive = true;
            }

            if (isLaserActive)
            {
                laserTime -= Time.deltaTime;
                if (laserTime < 0.5f)
                {
                    obj.SetActive(false);
                    isLaserActive = false;
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator DisplayPath()
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);

        yield return new WaitForSeconds(pathDisplayTime);

        lineRenderer.enabled = false;
        isDisplayingPath = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().GetDamage(10f);
        }
    }
}
