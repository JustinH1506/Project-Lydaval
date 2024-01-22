using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class NPCDialogues : MonoBehaviour
{
    private DialogueControllMap dialogueControllMap;
    
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private PlayableDirector _director;
    
    private bool inRange;
    
    /// <summary>
    /// dialogueControllMap is new DialogueControllMap.
    /// </summary>
    private void Awake()
    {
        dialogueControllMap = new DialogueControllMap();
    }

    /// <summary>
    /// We enable dialogueControllMap and subscribe it to Submit by performed and canceled. 
    /// </summary>
    private void OnEnable()
    {
        dialogueControllMap.Enable();

        dialogueControllMap.Dialogue.Submit.performed += StartDialogue;
        dialogueControllMap.Dialogue.Submit.canceled += StartDialogue;
    }

    /// <summary>
    /// We disable dialogueControllMap and deSubscribe it to Submit by performed and canceled. 
    /// </summary>
    private void OnDisable()
    {
        dialogueControllMap.Disable();
        
        dialogueControllMap.Dialogue.Submit.performed -= StartDialogue;
        dialogueControllMap.Dialogue.Submit.canceled -= StartDialogue;
        
    }

    /// <summary>
    /// We set inRange to true if player is in range.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    /// <summary>
    /// We set inRange to false if player is not in range.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    /// <summary>
    /// We set inRange to true if player is in range.
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    
    /// <summary>
    /// We set inRange to false if player is not in range.
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    /// <summary>
    /// If we press and are in range we start the dialogueMode and set inRange to false.
    /// </summary>
    /// <param name="context"></param>
    private void StartDialogue(InputAction.CallbackContext context)
    {
        if (context.performed && inRange)
        {
            dialogueManager.EnterDialogueMode(inkJSON, _director);
            inRange = false;
        }
    }
    
    /// <summary>
    /// We start the EnterDialogueMode method. 
    /// </summary>
    public void StartDialogueModeAndStopDirector()
    {
        dialogueManager.EnterDialogueMode(inkJSON, _director);
    }
}
