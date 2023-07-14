using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool isFollow = true;
    private void LateUpdate()
    {
        if (!isFollow)
            return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
    private void Start()
    {
        GameManager.inst.ResetPos += ResetPosCamera;
    }
    private void ResetPosCamera(Vector3 vec3)
    {
        transform.position -= vec3;
    }
}
