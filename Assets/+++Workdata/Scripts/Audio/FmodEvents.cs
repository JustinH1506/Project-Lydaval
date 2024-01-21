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

    public static FmodEvents instance { get; private set; }

    /// <summary>
    /// We ask if the instance isn´t null it shall give us a warning and we make this gameObject teh instance.
    /// </summary>
    private void Awake()
    {
        if( instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
