using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public Transform player;
    public Transform playerGun;
    public Transform playerTransformCamera;
    private Camera playerCamera;
    public Transform reticule;

    //Parameters
    [Range(1, 20)]
    public float aimRange;
    [Range(1, 20)]
    public float cameraRange;
    public AnimationCurve cameraOffsetSmoothing;

    //DEBUG
    Vector3 mouseViewportPos = Vector3.zero;
    Vector3 aimDirection = Vector3.zero;
    Vector3 aimPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = playerTransformCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Cursor.visible = false;
        mouseViewportPos = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        aimDirection = new Vector3(Mathf.Lerp(-1, 1, mouseViewportPos.x) * Screen.width / Screen.height, Mathf.Lerp(-1, 1, mouseViewportPos.y), 0);
        //aimDirection.x *= Screen.width / Screen.height;
        //Debug.Log("Viewport:"+mouseViewportPos+"    /aim:"+ aimDirection);

        Vector3 aimPosition = transform.position + aimDirection * aimRange;
        Vector3 cameraPosition = Vector3.Lerp(transform.position, transform.position + aimDirection * cameraRange, cameraOffsetSmoothing.Evaluate(aimDirection.magnitude / 1));




        reticule.position = aimPosition;
        playerTransformCamera.position = cameraPosition;



        //Gun Aim
        
        playerGun.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector3.right, reticule.position - playerGun.transform.position));
        
    }
}
