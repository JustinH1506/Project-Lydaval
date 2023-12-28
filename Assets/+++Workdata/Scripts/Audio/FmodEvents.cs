using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FmodEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: SerializeField] public EventReference music { get; private set; }

    [field: SerializeField] public EventReference startMusic { get; private set; }  

    [field: SerializeField] public EventReference lever { get; private set; }

    [field: SerializeField] public EventReference stoneDoor { get; private set; }

    [field: SerializeField] public EventReference footSteps { get; private set; }

    [field: SerializeField] public EventReference winSound { get; private set; }

    [field: SerializeField] public EventReference getItemSound { get; private set; }

    [field: SerializeField] public EventReference clickButton { get; private set; }

    public static FmodEvents instance { get; private set; }

    /// <summary>
    /// We ask if the instance isn´t null it shall give us a warning and we make this gameObject teh instance.
    /// </summary>
    private void Awake()
    {
        if( instance != null)
        {
            Debug.LogWarning("Found more than one FMODEvents!");
        }
        instance = this;
    }
}
