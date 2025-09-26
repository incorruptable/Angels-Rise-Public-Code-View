using UnityEngine;

public class ImageParallax : MonoBehaviour
{
    //Thought process for how to handle the parallax scrolling I intend, alongside the "treadmill logic".
    private float length, startPOS;
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float parallaxEff = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPOS = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = scrollSpeed * parallaxEff * Time.deltaTime;
        transform.position += Vector3.left * movement;


        if (transform.position.x <= startPOS - length) transform.position = new Vector3(startPOS, transform.position.y, transform.position.z);
    }
}


//dotProduct: If dotProduct < 0f, then entity has crossed the portal. If dotProduct > 0, then entity has NOT crossed the portal.