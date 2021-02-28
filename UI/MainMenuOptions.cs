using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer = null;
    [SerializeField] private GameObject tick = null;

    private float volume;
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Main Menu Music Volume", volume);
        this.volume = volume;
    }

    private void Update()
    {
        if(volume == -40)
        {
            tick.gameObject.SetActive(false);
        }
        else
        {
            tick.gameObject.SetActive(true);
        }
    }
}
