using System;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueAfterTimeline : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private PlayableDirector _director;

    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        playerMove.positionData.startCutsceneOff = true;
        
        dialogueManager.EnterDialogueMode(inkJSON, _director);
    }
}
