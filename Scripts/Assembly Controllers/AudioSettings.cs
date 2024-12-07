using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;
    public string parameterName;

    private void Awake()
    {
        // Loads saved volume level, or maxes the level if no value is found.
        float savedVol = PlayerPrefs.GetFloat(PlayerPrefsKeys.AudioVolume, slider.maxValue);
        SetVolume(savedVol);
        slider.value = savedVol;
        slider.onValueChanged.AddListener((float _) => SetVolume(_));
    }

    // Passes the information for the volume level to be saved in PlayerPrefs
    void SetVolume(float _value)
    {
        mixer.SetFloat(PlayerPrefsKeys.AudioVolume, ConvertToDecibel(_value / slider.maxValue));
        PlayerPrefs.SetFloat(PlayerPrefsKeys.AudioVolume, _value);
        Debug.Log(parameterName + ": " + PlayerPrefs.GetFloat(PlayerPrefsKeys.AudioVolume));
    }

    //Self Explanatory. Converts the volume value to a decibel value.
    public float ConvertToDecibel(float _value)
    {
        return Mathf.Log10(Mathf.Max(_value, 0.0001f)) * 20f;
    }
}
