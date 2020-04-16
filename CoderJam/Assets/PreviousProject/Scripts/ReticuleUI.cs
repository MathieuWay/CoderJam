using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticuleUI : MonoBehaviour
{
    public Transform reticule;
    private RectTransform rectTransform;
    public Image magazineCircle;
    private WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        weaponController = PlayerController.instance.weapon;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(reticule.position);
        if (weaponController.isReloading)
            magazineCircle.fillAmount = (float)(weaponController.weaponList[weaponController.currentWeapon].reloadCooldown - weaponController.reloadCooldown) / (float)weaponController.weaponList[weaponController.currentWeapon].reloadCooldown;
        else
            magazineCircle.fillAmount = (float)weaponController.magazineList[weaponController.currentWeapon] / (float)weaponController.weaponList[weaponController.currentWeapon].magazineSize;
    }
}
