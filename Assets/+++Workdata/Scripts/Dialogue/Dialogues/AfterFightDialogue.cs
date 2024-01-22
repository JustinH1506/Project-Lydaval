using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AfterFightDialogue : MonoBehaviour
{

    [SerializeField] private TextAsset afterTutorial, afterMcHome, afterBoss;

    [SerializeField] private PlayableDirector _director;

    [SerializeField] private DialogueManager _dialogueManager;
    
    /// <summary>
    /// Starts Dialogue with certain texts depending on enemy Managers Combat index
    /// </summary>
    private void Awake()
    {
        if (EnemyManager.instance.combatIndex == 2)
            StartDialogue(afterTutorial);
        else if (EnemyManager.instance.combatIndex == 3)
            StartDialogue(afterMcHome);
        else if(EnemyManager.instance.combatIndex == 4)
            StartDialogue(afterBoss);
    }

    /// <summary>
    /// enters DialogueMode with the story and the director. 
    /// </summary>
    /// <param name="story"></param>
    public void StartDialogue(TextAsset story)
    {
        _dialogueManager.EnterDialogueMode(story, _director);
    }
}
