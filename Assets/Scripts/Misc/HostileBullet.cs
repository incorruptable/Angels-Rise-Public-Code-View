using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileBullet : MonoBehaviour
{
    private Vector2 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector2(0f, Mathf.Sin(Time.time));
    }
}
