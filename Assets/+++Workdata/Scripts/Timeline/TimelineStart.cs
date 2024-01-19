using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStart : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _director.Play();
    }
}
