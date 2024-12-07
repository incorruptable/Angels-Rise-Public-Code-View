using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    private EnemyStats stats;
    private EnemyPathing pathing;
    private EnemyFiring firing;
    private EnemyHealth health;
    private EnemyAnimationManager animationManager;

    private void Awake()
    {
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
