using System;
using UnityEngine;

public class DialogueAfterTimeline : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private TextAsset inkJSON;

    private PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        playerMove.positionData.startCutsceneOff = true;
        
        dialogueManager.EnterDialogueMode(inkJSON);
        
        dialogueManager.ContinueStory();
    }
}
