using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveJump : MonoBehaviour
{
    Rigidbody rb;
    float hori;
    float verti;
    [SerializeField] float speed = 0;
    public GameObject test;


    [SerializeField] private float AngleSpeed;
    private float AngleRot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            verti = 1;
        else if (Input.GetKey(KeyCode.S))
            verti = -1;
        else
            verti = 0;
        if (Input.GetKey(KeyCode.D))
            hori = 1;
        else if (Input.GetKey(KeyCode.Q))
            hori = -1;
        else
            hori = 0;


        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(test, transform.position, transform.rotation);

            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero,1);
        }

        
    }

    private void FixedUpdate()
    {
        rb.velocity = ((Vector3.forward * verti + Vector3.right * hori).normalized * (speed * 1.0f / Time.fixedDeltaTime) * Time.fixedDeltaTime) + Vector3.up * rb.velocity.y;
        

        if (Mathf.Abs(rb.velocity.x) > 0.1 || Mathf.Abs(rb.velocity.z) > 0.1)
        {
            AngleRot = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, AngleRot, 0), Time.fixedDeltaTime * AngleSpeed);
    }



}
