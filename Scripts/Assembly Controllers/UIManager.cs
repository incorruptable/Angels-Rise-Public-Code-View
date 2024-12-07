using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    Player player;

    int playerHealth;

    void Awake()
    {
        playerHealth = player.GetHealth();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TookDamageUI()
    {
        this.playerHealth = player.GetHealth();
        UpdateUI();
    }

    public void UpdateUI()
    {
        livesText.text = playerHealth.ToString();
    }
}
