using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed;

    public float sensitivity = 100f;

    public float clampAngle = 70f;

    private float rotationX;
    private float rotationY;

    public Transform cameraTransform;

    public Vector3 dirNomalized;
    public Vector3 finalDir;

    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    private void Start()
    {
        rotationX = transform.localRotation.eulerAngles.x;
        rotationY = transform.localRotation.eulerAngles.y;

        dirNomalized = cameraTransform.localPosition.normalized;

        finalDistance = cameraTransform.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        rotationX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, - clampAngle, clampAngle);
        

        Quaternion rot = Quaternion.Euler(rotationX, rotationY, 0);

        transform.rotation = rot;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dirNomalized * maxDistance);

        RaycastHit hit;
        if(Physics.Linecast(transform.position,finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, dirNomalized * finalDistance, Time.deltaTime * smoothness);
    }
}
