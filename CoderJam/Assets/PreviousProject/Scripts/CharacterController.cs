using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Range(1, 10)]
    public float moveSpeed;
    [HideInInspector]
    public float moveFactor = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * moveFactor * Time.deltaTime);
    }
}
