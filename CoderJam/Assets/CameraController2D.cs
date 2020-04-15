using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float smoothSpeed = 0;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 cible = new Vector3(target.transform.position.x + offset.x, offset.y, offset.z);
        transform.position = Vector3.SmoothDamp(transform.position, cible, ref velocity, smoothSpeed, 5 ,Time.fixedDeltaTime);
    }
}
