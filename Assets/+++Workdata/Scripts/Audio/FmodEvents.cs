using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FmodEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference villageMusic { get; private set; }

    [field: SerializeField] public EventReference mainMenuMusic { get; private set; } 
    
    [field: SerializeField] public EventReference forestMusic { get; private set; }  
    
    [field: SerializeField] public EventReference battleMusic { get; private set; }  
    
    [field: SerializeField] public EventReference footSteps { get; private set; }  
    
    [field: SerializeField] public EventReference buttonSound { get; private set; }  
    
    [field: SerializeField] public EventReference hurtSound { get; private set; } 
    
    [field: SerializeField] public EventReference attackSound { get; private set; } 

    public static FmodEvents instance { get; private set; }

    /// <summary>
    /// We ask if the instance isn´t null it shall give us a warning and we make this gameObject the instance.
    /// </summary>
    private void Awake()
    {
        if( instance != null)
        {
            Debug.LogWarning("There are More");

            Destroy(gameObject);
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
