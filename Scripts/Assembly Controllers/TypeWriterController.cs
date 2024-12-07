using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TypeWriterController : MonoBehaviour
{
    [SerializeField]
    public float speed = 0.25f;
    [SerializeField]
    int scene;
    [SerializeField]
    AudioClip typingSFX;
    [SerializeField]
    [Range(0, 1)] float typeVolume = 0.5f;

    public AudioMixer audioMixer;


    public string fullText;
    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i < fullText.Length ;i++)
        {
            currentText = fullText.Substring(0,i);
            GetComponentInChildren<Text>().text = currentText;

            AudioSource.PlayClipAtPoint(typingSFX, Camera.main.transform.position, typeVolume);
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene);
    }
}
