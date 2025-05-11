using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField]
    Button[] menuControls;

    Game game;

    public void ActivateMissionCOntrols()
    {
        foreach (Button button in menuControls)
        {
            button.interactable = true;
        }
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        game.Load();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }

    public void ReturnMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void OpenControls()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
