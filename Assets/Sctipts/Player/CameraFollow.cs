using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject pivot;
    public float followSpeed;

    public float sensitivity = 100f;

    public float clampAngle = 40f;

    private float rotationX;
    private float rotationY;

    public Transform cameraTransform;

    public Vector3 dirNomalized;
    public Vector3 finalDir;

    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;

    public bool isInteraction;
    private void Start()
    {
        rotationX = transform.localRotation.eulerAngles.x;
        rotationY = transform.localRotation.eulerAngles.y;

        pivot = GameObject.FindGameObjectWithTag("CamPivot");

        

        dirNomalized = cameraTransform.localPosition.normalized;

        finalDistance = cameraTransform.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        if(!isInteraction) //��ȣ�ۿ� �ϰ� ���� ���� ��
        {
            rotationX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, 0, clampAngle);
            //rotationY  = Mathf.Clamp(rotationY, - clampAngle, clampAngle);

            Quaternion rot = Quaternion.Euler(rotationX, rotationY, 0);

            transform.rotation = rot;
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, pivot.transform.position, followSpeed * Time.deltaTime);
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