using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance;
    
    FMOD.Studio.EventInstance seTestEvent;

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Music;

    public Data data;
    
    [System.Serializable]
    public class Data
    {
        public float musicVolume = 0.5f;
        public float SFXVolume = 0.5f;
        public float masterVolume = 1f;
    }

    /// <summary>
    /// Getting the busses from FMOD
    /// </summary>
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
    }

    private void Start()
    {
        var loadedData = GameStateManager.instance.data.audioData;
        if (loadedData != null)
        {
            data = loadedData;
        }

        GameStateManager.instance.data.audioData = data;
    }

    /// <summary>
    /// Seting the float as Volume to the busses
    /// </summary>
    void Update()
    {
        Music.setVolume(data.musicVolume);
        SFX.setVolume(data.SFXVolume);
        Master.setVolume(data.masterVolume);
    }


    /// <summary>
    /// Method to Change the Volume based on the new value
    /// </summary>
    /// <param name="newMasterVolume"></param>
    public void MasterVolumeLevel(float newMasterVolume)
    {
        data.masterVolume = newMasterVolume;
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
    }

    /// <summary>
    /// Method to Change the Volume based on the new value
    /// </summary>
    /// <param name="newMasterVolume"></param>
    public void MusicVolumeLevel(float newMusicVolume)
    {
        data.musicVolume = newMusicVolume;
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);

    }

    /// <summary>
    /// Method to Change the Volume based on the new value
    /// </summary>
    /// <param name="newMasterVolume"></param>
    public void SFXVolumeLevel(float newSFXVolume)
    {
        data.SFXVolume = newSFXVolume;
        
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);

    }
}
