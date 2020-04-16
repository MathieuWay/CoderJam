using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public static WeaponUI instance;
    public Text weaponName;
    public Image weaponImage;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadWeaponUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadWeaponUI()
    {
        WeaponPreset weapon = PlayerController.instance.weapon.weaponList[PlayerController.instance.weapon.currentWeapon];
        weaponName.text = weapon.weaponName;
        weaponImage.sprite = weapon.sprite;
        weaponImage.SetNativeSize();
        UpdateMagazineUI();
    }

    public void UpdateMagazineUI()
    {

    }
}
