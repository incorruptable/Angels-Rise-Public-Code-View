using UnityEngine;

public class ParallaxPortal : MonoBehaviour
{
    [SerializeField] GameObject linkedPortal;

    //This is actually meant to be something different than it's named. It's not a Parallax.
    //It's a reflector/portal. If something impacts the primary object, it's meant to reflect or have the item pass through another point.
    //Some enemies may have a mirror defense or a portal defense, similar to how portals in Portal work.

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
