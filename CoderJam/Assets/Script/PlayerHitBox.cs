using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Obstacle":
                PlayerController.instance.life -= 10;
                break;
            case "Weapon":
                Debug.Log("Grab");
                PlayerController.instance.weapon.GrabWeapon(collision.GetComponent<WeaponToGrab>().weaponPreset);
                Destroy(collision.gameObject);
                break;
        }
    }
}
