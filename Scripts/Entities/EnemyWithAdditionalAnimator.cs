using UnityEngine;

public class EnemyWithAdditionalAnimator : Enemy
{
    [SerializeField] GameObject additionalObject;  // Should be assigned to the tongue object
    private Animator additionalAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    protected override void Start()
    {
        base.Start();

        if (additionalObject != null)
        {
            // Find and assign the Animator component for the additional object
            additionalAnimator = additionalObject.GetComponent<Animator>();
            if (additionalAnimator == null)
            {
                Debug.LogWarning($"Animator component not found on additionalObject for {gameObject.name}.");
            }
            else
            {
                // Set layer weight to ensure it is active for Tongue Layer
                additionalAnimator.SetLayerWeight(1, 1f);
                Debug.Log($"Tongue Layer weight set to 1 for {additionalObject.name}");
            }

            // Ensure SpriteRenderer is enabled for visibility
            SpriteRenderer sr = additionalObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;  // Ensure visibility
                sr.sortingLayerName = "Default";  // Set to appropriate sorting layer
                sr.sortingOrder = 0;  // Adjust this value based on desired layering
                Debug.Log($"SpriteRenderer enabled for {additionalObject.name} with Sorting Layer {sr.sortingLayerName} and Order {sr.sortingOrder}");
            }
            else
            {
                Debug.LogWarning($"SpriteRenderer component not found on {additionalObject.name}.");
            }
        }
        else
        {
            Debug.LogWarning($"additionalObject (Tongue) is not assigned in the Inspector for {gameObject.name}.");
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!isDead)
        {
            ActivateAdditionalAnimation();
        }
        else
        {
            DeactivateAdditionalAnimation();
        }
    }

    private void ActivateAdditionalAnimation()
    {
        if (additionalAnimator != null && !isDead)
        {
            additionalAnimator.SetLayerWeight(1, 1f);
            SpriteRenderer sr = additionalObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;  // Ensure visibility in case it's turned off elsewhere
            }
        }
    }

    private void DeactivateAdditionalAnimation()
    {
        if (additionalAnimator != null && isDead)
        {
            additionalAnimator.SetLayerWeight(1, 0f);
        }
    }
    */
}
