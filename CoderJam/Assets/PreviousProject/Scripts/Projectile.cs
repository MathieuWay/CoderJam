using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (range <= 0)
            Destroy(gameObject);
        float distance;
        if (range - speed * Time.deltaTime < 0)
            distance = range;
        else
            distance = speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance);
        if(hit.collider != null)
        {

            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, distance);
            range -= distance;
        }
    }

    public void Init(float speed, float range)
    {
        this.speed = speed;
        this.range = range;
    }
}
