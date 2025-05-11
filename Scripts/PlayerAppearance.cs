using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAppearance", menuName = "Player/Appearance")]
public class PlayerAppearance : ScriptableObject
{
    [Header("Main Body Sprites")]
    public Sprite[] bodySprites;

    [Header("Wing Sprites")]
    public Sprite wingSprites;

    [Header("Engine and Dash Effects")]
    public GameObject engineEffect;
    public Sprite dashEffect;

    [Header("Animation Controller")]
    public RuntimeAnimatorController animationController;
}