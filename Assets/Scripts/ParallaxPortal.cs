using UnityEngine;

public class ParallaxPortal : MonoBehaviour
{
    [SerializeField] GameObject linkedPortal;

    Vector2 entityDirection, portalNormal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        portalNormal = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            entityDirection = rb.linearVelocity;
        }
        else
        {
            Vector2 otherPos = new Vector2(other.transform.position.x, other.transform.position.y);
            entityDirection = (otherPos - portalNormal).normalized;
        }
    }

    private void PortalMethod()
    {
        if(linkedPortal == null)
        {
            //Methodology is to act as a reflecting surface as a mirror.
        }
        else
        {
            //Apply Portal logic
        }
    }
}
