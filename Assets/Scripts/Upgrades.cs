using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

//Legacy code.
//This is the Unity based method of upgrading logic. Pushes the logic into the Player script.

public class Upgrades : MonoBehaviour
{
    [SerializeField]
    GameObject shieldPrefab;
    [SerializeField]
    int health = 5;
    [SerializeField]
    GameObject upgradePrefab;

    [SerializeField]
    AudioClip damageSFX;
    [SerializeField]
    [Range(0, 1)] float damageVolume = 0.7f;

    public UpgradeFlag upgradeType;
    public WeaponFlag weaponType;

    private void AddShield(GameObject player)
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity) as GameObject;
    }

    public void AddUpgrade(GameObject obj)
    {
        Player playerScript = obj.GetComponent<Player>();

        if (playerScript != null)
        {
            playerScript.ApplyUpgrade(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AddUpgrade(other.gameObject);
        }
    }
}
