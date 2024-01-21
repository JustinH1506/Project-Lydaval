using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStart : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private ObjectData _objectData;
 
    public ObjectStates objectStates;

    private void Awake()
    {
        _objectData.data.objectStates = objectStates;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerMove.rb.velocity = new Vector2(0f, 0f);
        
        _director.Play();
        
        _objectData.ChangeQuestTracker();
    }
}
