using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManagers : MonoBehaviour
{
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

    public static bool isSoundOn = true;
    private string soundKey = "SoundState";

    private void Start()
    {
        if (PlayerPrefs.HasKey(soundKey))
        {
            isSoundOn = PlayerPrefs.GetInt(soundKey) == 1;
        }

        soundImage.sprite = isSoundOn ? soundOn : soundOff;
        AudioListener.volume = isSoundOn ? 1.0f : 0.0f;
    }

    public void SwitchSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt(soundKey, isSoundOn ? 1 : 0);
        soundImage.sprite = isSoundOn ? soundOn : soundOff;
        AudioListener.volume = isSoundOn ? 1.0f : 0.0f;
    }



}
