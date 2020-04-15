using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 0;
    private float currentSpeed;

    private Vector2 pointS;
    private Vector2 pointH;
    private Vector2 pointF;

    [SerializeField] private float hauteur;
    [SerializeField] private float distance;
    private float ratioDH = 1;
    private List<Vector2> pointCourbe = new List<Vector2>();
    float DrawTime = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pointS = transform.position;
    }

    private void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdatePointCourbe();
        }
        Courbe();
        for (int i = 1; i < pointCourbe.Count; i++)
        {
            Debug.DrawLine(pointCourbe[i - 1], pointCourbe[i], Color.green);
        }

    }

    void UpdatePointCourbe()
    {
        pointS = transform.position;
        pointH = new Vector2(pointS.x + (distance / 2) * ratioDH, pointS.y + hauteur * ratioDH);
        pointF = new Vector2(pointS.x + distance * ratioDH, pointS.y);
        pointCourbe.Clear();
        DrawTime = 0;
        currentSpeed = 0;
        StartCoroutine(Calcule());
    }

    void Courbe()
    {
        if(currentSpeed < 1)
        {
            Vector2 pointA = Vector2.Lerp(pointS, pointH, currentSpeed);
            Vector2 pointB = Vector2.Lerp(pointH, pointF, currentSpeed);
            transform.position = Vector2.Lerp(pointA, pointB, currentSpeed);
            currentSpeed += Time.deltaTime * speed;
        }
    }


    IEnumerator Calcule()
    {
        while(DrawTime <= 1)
        {
            Vector2 pointA = Vector2.Lerp(pointS, pointH, DrawTime);
            Vector2 pointB = Vector2.Lerp(pointH, pointF, DrawTime);
            pointCourbe.Add(Vector2.Lerp(pointA, pointB, DrawTime));
            DrawTime += Time.deltaTime;    
        }
        yield return null;
    }
}
