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

    /// <summary>
    /// Set velocity og Player to 0, Play the director, make ObjectStates equal to these objectStates,
    /// Call ChangeQuestTracker from objectData. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerMove.rb.velocity = new Vector2(0f, 0f);
        
        _director.Play();

        _objectData.data.objectStates = objectStates;
        
        _objectData.ChangeQuestTracker();
    }
}
