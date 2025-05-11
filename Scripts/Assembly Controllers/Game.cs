using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int startLives;
    [SerializeField]
    private TextMeshProUGUI livesText;

    int playerLives;

    void Start()
    {
        playerLives = startLives;
    }

    void Awake()
    {
        Load();
    }

    public void LifeLost()
    {
        playerLives = playerLives - 1;
        Debug.Log("A life has been lost");
        UpdateUI();
        if (playerLives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(4);
            PlayerPrefs.DeleteAll();
        }
    }

    public void AddLife()
    {
        playerLives += 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        livesText.text = playerLives.ToString();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Lives", playerLives);
    }

    public void Load()
    {
        playerLives = PlayerPrefs.GetInt(PlayerPrefsKeys.Lives, startLives);
        UpdateUI();
    }
}
