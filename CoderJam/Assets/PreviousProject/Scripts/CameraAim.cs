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
    public float aimRangeX;
    [Range(1, 20)]
    public float aimRangeY;
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
        if (PlayerController.instance.weapon.weaponList[PlayerController.instance.weapon.currentWeapon])
        {
            Cursor.visible = false;
            mouseViewportPos = playerCamera.ScreenToViewportPoint(Input.mousePosition);
            aimDirection = new Vector3(Mathf.Lerp(-1, 1, mouseViewportPos.x) * Screen.width / Screen.height, Mathf.Lerp(-1, 1, mouseViewportPos.y), 0);
            //aimDirection.x *= Screen.width / Screen.height;
            //Debug.Log("Viewport:"+mouseViewportPos+"    /aim:"+ aimDirection);
            Vector3 aimRange = new Vector3(aimRangeX, aimRangeY, 0);
            //Reticule position
            Vector3 aimPosition = aimDirection;
            aimPosition.Scale(aimRange);
            aimPosition += transform.position;
            reticule.position = aimPosition;

            //Camera Position
            Vector3 cameraPosition = aimDirection;
            cameraPosition.Scale(new Vector3(aimRangeX - (Camera.main.orthographicSize * (Screen.width / Screen.height)), aimRangeY - Camera.main.orthographicSize));
            cameraPosition += transform.position;
            playerTransformCamera.position = Vector3.Lerp(transform.position, cameraPosition, cameraOffsetSmoothing.Evaluate(aimDirection.magnitude / 1));
            //Gun Aim
            playerGun.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector3.right, reticule.position - playerGun.transform.position));
        }
        else
        {
            reticule.position = Vector3.zero;
            playerTransformCamera.position = PlayerController.instance.transform.position + Vector3.up *2;
        }
    }
}
