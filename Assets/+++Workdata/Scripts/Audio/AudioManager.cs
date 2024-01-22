using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private EventInstance forestMusicEventInstance;
    
    private EventInstance villageMusicEventInstance;
    
    private EventInstance mainMenuMusicEventInstance;
    
    private EventInstance battleMusicEventInstance;

    public static AudioManager instance { get; private set; }    

    /// <summary>
    /// We ask if the instance isn´t null it shall give us a warning and we make this gameObject the instance.
    /// </summary>
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There are More");
            Destroy(gameObject);
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        
        DontDestroyOnLoad(gameObject);
    }

     /// <summary>
    /// Plays the PlayOneShot sound.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="worldPos"></param>
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);    
    }

    /// <summary>
    /// Set local EventInstance equal to runtime manager.
    /// Give that eventInstance to eventInstances and return the eventInstance.  
    /// </summary>
    /// <param name="eventReference"></param>
    /// <returns></returns>
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);

        eventInstances.Add(eventInstance);  

        return eventInstance;
    }

    /// <summary>
    /// Start the maineMenu Music.
    /// </summary>
    private void Start()
    {
        InitializeMusic(FmodEvents.instance.mainMenuMusic);
    }

    /// <summary>
    /// We call the CreateInstance Method to make our musicEventInstance to this and start it at the start method.
    /// </summary>
    /// <param name="musicEventReference"></param>
    public void InitializeMusic(EventReference musicEventReference)
    {
        forestMusicEventInstance = CreateInstance(musicEventReference);
        forestMusicEventInstance.start();
    }

    /// <summary>
    /// Stopping the sounds when entering other scenes.
    /// </summary>
    public void CleanUp()
    {
        foreach(EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
}