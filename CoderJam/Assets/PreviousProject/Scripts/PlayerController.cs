using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Reference
    public static PlayerController instance;
    [HideInInspector]
    public CharacterController character;
    [HideInInspector]
    public WeaponController weapon;

    //Stamina
    public float maxStamina = 100;
    public float consumeRateStamina = 10;
    [HideInInspector]
    public float currentStamina = 100;

    //Switch Weapon Cooldown
    public float switchWeaponCooldown;
    [HideInInspector]
    public float switchWeaponTimer;

    // Start is called before the first frame update
    void Awake()
    {
        character = GetComponent<CharacterController>();
        weapon = GetComponent<WeaponController>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        #region Stamina
        if (currentStamina > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            character.moveFactor = 2;
            currentStamina -= consumeRateStamina * Time.deltaTime;
        }
        else
        {
            character.moveFactor = 1;
            if (currentStamina < maxStamina)
                currentStamina += consumeRateStamina * Time.deltaTime;
            else
                currentStamina = maxStamina;
        }
        #endregion

        #region Weapon Selection
        if (switchWeaponTimer < 0)
        {
            float scrollAxisValue = Input.GetAxisRaw("Mouse ScrollWheel");
            if (scrollAxisValue > 0)
            {
                if (weapon.weaponList[(weapon.currentWeapon + 1) % weapon.weaponList.Length])
                {
                    weapon.currentWeapon = (weapon.currentWeapon + 1) % weapon.weaponList.Length;
                    WeaponUI.instance.LoadWeaponUI();
                    switchWeaponTimer = switchWeaponCooldown;
                }
            }
            else if (scrollAxisValue < 0)
            {
                if(weapon.currentWeapon > 0)
                {

                    if (weapon.weaponList[weapon.currentWeapon - 1])
                    {
                        weapon.currentWeapon--;
                        WeaponUI.instance.LoadWeaponUI();
                        switchWeaponTimer = switchWeaponCooldown;
                    }
                }
                else
                {
                    if (weapon.weaponList[weapon.weaponList.Length - 1])
                    {
                        weapon.currentWeapon = weapon.weaponList.Length - 1;
                        WeaponUI.instance.LoadWeaponUI();
                        switchWeaponTimer = switchWeaponCooldown;
                    }
                }
            }
        }
        else
            switchWeaponTimer -= Time.deltaTime;
        #endregion

        #region Weapon Reload
        if (!weapon.isReloading)
        {
            if (weapon.weaponList[weapon.currentWeapon] && Input.GetKeyDown(KeyCode.Space))
            {
                weapon.isReloading = true;
                weapon.reloadCooldown = weapon.weaponList[weapon.currentWeapon].reloadCooldown;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                weapon.isCancelingReload = true;
            }
            if (weapon.reloadCooldown > 0)
                weapon.reloadCooldown -= Time.deltaTime;
            else
            {
                if (weapon.weaponList[weapon.currentWeapon].reloadOneByOne && weapon.magazineList[weapon.currentWeapon] < weapon.weaponList[weapon.currentWeapon].magazineSize)
                {
                    if (weapon.isCancelingReload)
                    {
                        weapon.reloadCooldown = 0;
                        weapon.isReloading = false;
                        weapon.isCancelingReload = false;
                    }
                    else
                        weapon.reloadCooldown = weapon.weaponList[weapon.currentWeapon].reloadCooldown;
                    weapon.magazineList[weapon.currentWeapon]++;
                }
                else
                {
                    weapon.reloadCooldown = 0;
                    weapon.magazineList[weapon.currentWeapon] = weapon.weaponList[weapon.currentWeapon].magazineSize;
                    weapon.isReloading = false;
                }
            }
        }
        #endregion
    }
}
