using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    //Enemy is more complex in design than the player.
    //It's meant to show single responsibility principle.
    //This was done AFTER the creation of the Player, which would require significant updates and disentanglement to work properly.
    private EnemyStats stats;
    private EnemyPathing pathing;
    private EnemyFiring firing;
    private EnemyHealth health;
    private EnemyAnimationManager animationManager;

    private void Awake()
    {
        //Pulls the appropriate components attached to the Enemy actor.
        stats = GetComponent<EnemyStats>();
        pathing = GetComponent<EnemyPathing>();
        firing = GetComponent<EnemyFiring>();
        animationManager = GetComponent<EnemyAnimationManager>();
        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        firing.Initialize(stats, animationManager);
        pathing.Initialize();
        health.Initialize(stats, animationManager);
    }

    private void Update()
    {
        if (!health.IsDead)
        {
            pathing.HandlePathing();
            firing.HandleFiring();
        }
    }
}
