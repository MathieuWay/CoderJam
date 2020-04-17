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
    bool jumping;
    //Scrolling Movement
    public float startScrollingSpeed = 1;
    public float endScrollingSpeed = 1;
    [HideInInspector] public float currentScrollingSpeed;
    public float duration;
    public AnimationCurve scrollingSpeedCurve;
    private float startTime;
    private float durationRatio;
    private float scrollingSpeed;
    private Animator anim;

    public bool GameStarted;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        pointS = transform.position;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && jumping)
        {

        }
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            if (!GameStarted)
                StartCoroutine("StartGame");
            else
            {
                anim.SetBool("jumping", true);
                jumping = true;
                UpdatePointCourbe();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameStarted) return;
        if (pointCourbe.Count > 0)
        Courbe();
        #region Scrolling Movement
        float timeRatio = (Time.time - startTime) / duration;
        currentScrollingSpeed = Mathf.Lerp(startScrollingSpeed, endScrollingSpeed, scrollingSpeedCurve.Evaluate(timeRatio));
        //Debug.Log("Time Ratio:" + timeRatio + "     / Scrolling Speed:" + currentScrollingSpeed);
        transform.position += Vector3.right * currentScrollingSpeed * Time.fixedDeltaTime;
        #endregion
        //Physics2D.BoxCastAll
    }

    void UpdatePointCourbe()
    {
        pointS = transform.position;
        pointH = new Vector2(pointS.x + (distance / 2) * ratioDH, pointS.y + (hauteur) * ratioDH);
        pointF = new Vector2(pointS.x + distance     * ratioDH, pointS.y);
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
        anim.SetFloat("curveRatio", ratioTime);
        if (ratioTime <= 1)
        {
            Vector2 pointA = Vector2.Lerp(pointS, pointH, ratioTime);
            Vector2 pointB = Vector2.Lerp(pointH, pointF, ratioTime);
            float offset = PlayerController.instance.currentStamina / 50;
            transform.position = new Vector2(transform.position.x, Vector2.Lerp(pointA, pointB, ratioTime).y + offset);
        }
        else if (jumping)
        {
            anim.SetBool("jumping", false);
            jumping = false;
            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;
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

    IEnumerator StartGame()
    {
        anim.SetTrigger("StartGame");
        yield return new WaitForSeconds(3f);
        GameStarted = true;
        startTime = Time.time;
        anim.SetBool("gameStarted", true);
    }
}
