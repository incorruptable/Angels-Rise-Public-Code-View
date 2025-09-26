using UnityEditor;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private Animator mainAnimator;
    private int chargingLayerIndex;

    [Header("Additional Animators and Objects")]
    [SerializeField] private GameObject[] additionalObjects;
    [SerializeField] private string chargingStateName = "Charging";
    private Animator[] additionalAnimators;
    private SpriteRenderer[] additionalSpriteRenderers;

    private void Start()
    {
        // Initialize the main animator
        mainAnimator = GetComponent<Animator>();
        if (mainAnimator != null)
        {
            chargingLayerIndex = GetAnimatorLayerIndex(mainAnimator, "Charging");
        }

        // Initialize additional animators and sprite renderers
        if (additionalObjects != null && additionalObjects.Length > 0)
        {
            additionalAnimators = new Animator[additionalObjects.Length];
            additionalSpriteRenderers = new SpriteRenderer[additionalObjects.Length];

            for (int i = 0; i < additionalObjects.Length; i++)
            {
                if (additionalObjects[i] != null)
                {
                    additionalAnimators[i] = additionalObjects[i].GetComponent<Animator>();
                    additionalSpriteRenderers[i] = additionalObjects[i].GetComponent<SpriteRenderer>();
                }
            }
        }
    }

    // Charging Animation Management
    public void StartChargingAnimation()
    {
        bool usedAdditionalAnimator = false;

        if(additionalAnimators != null)
        {
            foreach(Animator animator in additionalAnimators)
            {
                if(animator != null && animator.gameObject.name.Contains("Charge"))
                {
                    animator.SetBool("isCharging", true);
                    animator.Play(chargingStateName, 0, 0f);
                    Debug.Log($"Running the charging animator on {animator.gameObject.name}");
                    usedAdditionalAnimator = true;
                }
                else if (animator != null)
                {
                    string objectName = animator.gameObject.name.ToLower();

                    foreach (var clip in animator.runtimeAnimatorController.animationClips)
                    {
                        if (name.Contains(clip.name.ToLower()) && !name.Contains("Charge"))
                        {
                            animator.Play(clip.name, 0, 0f);
                            Debug.Log($"Matched {clip.name} to {animator.gameObject.name}");
                        }
                    }
                    var clips = animator.runtimeAnimatorController.animationClips;
                }
            }
        }

        if(!usedAdditionalAnimator && mainAnimator != null && chargingLayerIndex != -1)
        {
            mainAnimator.SetLayerWeight(chargingLayerIndex, 1f);
            mainAnimator.SetBool("isCharging", true);
            Debug.Log($"Running the charging animator on {mainAnimator.gameObject.name}");
            mainAnimator.Play(chargingStateName, chargingLayerIndex, 0f);
        }
    }

    public void StopChargingAnimation()
    {
        bool usedAdditionalAnimator = false;
        if (additionalAnimators != null)
        {
            foreach (Animator animator in additionalAnimators)
            {
                if (animator != null)
                {
                    animator.SetBool("isCharging", false);
                    usedAdditionalAnimator = true;
                }
            }
        }

        if (!usedAdditionalAnimator && mainAnimator != null && chargingLayerIndex != -1)
        {
            mainAnimator.SetLayerWeight(chargingLayerIndex, 0f);
        }
    }

    // Death Animation
    public void PlayDeathAnimation()
    {
        if (mainAnimator != null)
        {
            mainAnimator.SetBool("isDead", true);
        }

        HandleAdditionalObjectsVisibility(false); // Hide additional objects upon death
    }

    // Additional Animators Management
    public void PlayAdditionalAnimation(string animationName, int index)
    {
        if (index >= 0 && index < additionalAnimators.Length && additionalAnimators[index] != null)
        {
            additionalAnimators[index].Play(animationName);
        }
        else
        {
            Debug.LogWarning($"Invalid additional animator index {index} or animator not found.");
        }
    }

    public void SetAdditionalAnimationLayerWeight(int index, int layerIndex, float weight)
    {
        if (index >= 0 && index < additionalAnimators.Length && additionalAnimators[index] != null)
        {
            additionalAnimators[index].SetLayerWeight(layerIndex, weight);
        }
        else
        {
            Debug.LogWarning($"Invalid additional animator index {index} or animator not found.");
        }
    }

    public void SetAdditionalAnimationBool(string parameterName, bool value, int index)
    {
        if (index >= 0 && index < additionalAnimators.Length && additionalAnimators[index] != null)
        {
            additionalAnimators[index].SetBool(parameterName, value);
        }
        else
        {
            Debug.LogWarning($"Invalid additional animator index {index} or animator not found.");
        }
    }

    // Visibility Management
    public void HandleMainVisibility(bool isVisible)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = isVisible;
        }
    }

    public void HandleAdditionalObjectsVisibility(bool isVisible)
    {
        if (additionalSpriteRenderers != null)
        {
            foreach (SpriteRenderer sr in additionalSpriteRenderers)
            {
                if (sr != null)
                {
                    sr.enabled = isVisible;
                }
            }
        }
    }

    // Utility: Get Layer Index by Name
    private int GetAnimatorLayerIndex(Animator animator, string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            if (animator.GetLayerName(i) == layerName)
            {
                return i;
            }
        }
        return -1;
    }
}
