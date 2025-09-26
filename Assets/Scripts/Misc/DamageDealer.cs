using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    //Generic script for things meant to "deal damage", like a bullet or a moving obstacle that can collide with the player.

    [SerializeField] int damage = 1;
    [SerializeField] string tagCheck;
    [SerializeField] string ignoreTag;
    private Collider2D projectileCollider;

    private void Start()
    {
        projectileCollider = GetComponent<Collider2D>();

        if (projectileCollider == null) Debug.LogError($"No Collider2D attached to {gameObject.name}. DamageDealer will not initiate.");
    }

    public int getDamage()
    {
        return damage;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (string.IsNullOrEmpty(otherCollider.tag)) return;

        if (otherCollider.CompareTag(ignoreTag)) return;

            if (otherCollider.CompareTag(tagCheck))
        {
            Debug.Log("Detected collision.");
            Destroy(this.gameObject);
        }

    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}