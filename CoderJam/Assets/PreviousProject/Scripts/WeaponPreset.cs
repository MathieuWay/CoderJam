using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "New Weapon")]
public class WeaponPreset : ScriptableObject
{
    public string weaponName;
    public Sprite sprite;
    public int damageOnHit;
    [Tooltip("Rate of Fire Per Minute")]
    public float rofPerMinute;
    public int magazineSize;
    public float accuracyAngle;
    public int projectileEachShot = 1;
    public float projectileSpeed;
    public float projectileRange;
    public float reloadCooldown;
    public bool reloadOneByOne;
}
