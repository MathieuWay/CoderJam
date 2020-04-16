using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform muzzle;
    public Transform gunAnchor;
    public Transform projectileFolder;
    public GameObject projectilePrefab;
    public WeaponPreset[] weaponList = new WeaponPreset[4];
    public int[] magazineList = new int[4];
    public int currentWeapon = 0;

    private float shotTimer = 0f;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public bool isCancelingReload = false;
    public float reloadCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            magazineList[i] = weaponList[i].magazineSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponList[currentWeapon]) return;

        if (shotTimer > 0)
            shotTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && magazineList[currentWeapon] > 0)
        {
            if (shotTimer <= 0 && reloadCooldown <= 0)
            {
                Shoot();
                shotTimer = 60 / weaponList[currentWeapon].rofPerMinute;
            }
        }
    }

    private void Shoot()
    {
        magazineList[currentWeapon]--;
        for (int i = 0; i < weaponList[currentWeapon].projectileEachShot; i++)
        {
            Vector3 direction = gunAnchor.rotation.eulerAngles;
            direction.z += UnityEngine.Random.Range(-weaponList[currentWeapon].accuracyAngle / 2, weaponList[currentWeapon].accuracyAngle / 2);
            GameObject gameObject = Instantiate(projectilePrefab, muzzle.position, Quaternion.Euler(direction), projectileFolder);
            Projectile projectile = gameObject.GetComponent<Projectile>();
            projectile.Init(weaponList[currentWeapon].projectileSpeed, weaponList[currentWeapon].projectileRange);
        }
    }
}
