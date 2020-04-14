using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float distance = 0;
    [SerializeField] private float height = 0;

    private void Update()
    {
        //transform.eulerAngles = new Vector3(0,0,height);
        transform.position = target.position;
        transform.position -= Quaternion.Euler(height,0,0) * Vector3.forward * distance;
        transform.LookAt(target);
    }
}
