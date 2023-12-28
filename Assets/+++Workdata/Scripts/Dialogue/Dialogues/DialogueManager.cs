using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public bool dialogueIsPlaying { get; private set; }
    
    private Story currentStory;

    private DialogueControllMap dialogueControllMap;

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        dialogueControllMap = new DialogueControllMap();
    }

    private void OnEnable()
    {
        dialogueControllMap.Enable();

        dialogueControllMap.Dialogue.Submit.performed += Submit;
        dialogueControllMap.Dialogue.Submit.canceled += Submit;
    }

    private void OnDisable()
    {
        dialogueControllMap.Disable();
        
        dialogueControllMap.Dialogue.Submit.performed -= Submit;
        dialogueControllMap.Dialogue.Submit.canceled -= Submit;
        
    }
    
     public void Update()
     {
         if(!dialogueIsPlaying) 
         {
             return;
         }
     }

     public void EnterDialogueMode(TextAsset inkJSON)
     {
         currentStory = new Story(inkJSON.text);

         dialoguePanel.SetActive(true);
         
         dialogueIsPlaying = true;
         
         ContinueStory();
     }
     
     private IEnumerator ExitDialogueMode()
     {
         yield return new WaitForSeconds(2f);

         dialogueIsPlaying = false;

         dialoguePanel.SetActive(false);
     }
     
     public void ContinueStory()
     {
         if (currentStory.canContinue)
         {
             dialogueText.text = currentStory.Continue();
         }
         else
         {
             StartCoroutine(ExitDialogueMode());
         }
     }
     
     public void Submit(InputAction.CallbackContext context)
     {
         if (context.performed && dialogueIsPlaying)
         {
             ContinueStory();
         }
     }
}
