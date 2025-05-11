using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioClip;

    private AudioSource currentAudio;

    void Start()
    {
        Music();
    }

    void Music()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //Checks if the audio that's present should be playing. If no, it changes to the assigned audio for the set up
            if (this.currentAudio != audioClip && this.currentAudio != null)
            {
                this.currentAudio.clip = audioClip.clip;
                //gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music",1f);
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
