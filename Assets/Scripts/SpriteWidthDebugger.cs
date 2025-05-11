using UnityEngine;

public class SpriteWidthDebugger : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            float width = sr.bounds.size.x;
            Debug.Log("Sprite width in world units: " + width);
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on this GameObject.");
        }
    }
}
