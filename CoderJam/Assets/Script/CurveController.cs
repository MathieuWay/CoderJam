using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 0;
    private float currentTime;

    private Vector2 pointS;
    private Vector2 pointH;
    private Vector2 pointF;
    float ratioTime;
    [SerializeField] private float hauteur;
    [SerializeField] private float distance;
    private float ratioDH = 1;
    private List<Vector2> pointCourbe = new List<Vector2>();
    float DrawTime = 0;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        pointS = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpdatePointCourbe();
        }
        for (int i = 1; i < pointCourbe.Count; i++)
        {
            Debug.DrawLine(pointCourbe[i - 1], pointCourbe[i], Color.green);
        }
    }

    private void FixedUpdate()
    {      
        if(pointCourbe.Count > 0)
        Courbe();
        //Physics2D.BoxCastAll
    }

    void UpdatePointCourbe()
    {
        pointS = transform.position;
        pointH = new Vector2(pointS.x + (distance / 2) * ratioDH, pointS.y + (hauteur) * ratioDH);
        pointF = new Vector2(pointS.x + distance * ratioDH, pointS.y);
        pointCourbe.Clear();
        DrawTime = 0;
        currentTime = 0;
        StartCoroutine(Calcule());
    }

    void Courbe()
    {
        float totalTime = (pointF - pointS).magnitude / speed;
        currentTime += Time.fixedDeltaTime;
        ratioTime = currentTime / totalTime;
        Vector2 pointA = Vector2.Lerp(pointS, pointH, ratioTime);
        Vector2 pointB = Vector2.Lerp(pointH, pointF, ratioTime);
        transform.position = Vector2.Lerp(pointA, pointB, ratioTime);
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
