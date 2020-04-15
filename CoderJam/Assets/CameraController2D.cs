using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float smoothSpeed = 0;

    private void FixedUpdate()
    {
        Vector3 PosDesired = Vector3.Lerp(transform.position, target.transform.position + offset, smoothSpeed);
        transform.position = new Vector3(PosDesired.x, PosDesired.y, transform.position.z);
        transform.LookAt(target.transform);
    }
}
