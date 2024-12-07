using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    Button[] menuControls;
    [SerializeField]
    Button[] SkipButton;

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

    public void SkipScene()
    {
        SceneManager.LoadScene(4);
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
