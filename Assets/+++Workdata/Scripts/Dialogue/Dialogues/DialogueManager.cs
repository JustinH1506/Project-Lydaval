using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Ink.Runtime;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    public bool dialogueIsPlaying { get; private set; }
    
    private Story currentStory;

    private DialogueControllMap dialogueControllMap;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private PlayableDirector mainDirector;

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

     public void EnterDialogueMode(TextAsset inkJSON, PlayableDirector director)
     {
         mainDirector = director;
         
         director.Pause();
         
         currentStory = new Story(inkJSON.text);

         dialoguePanel.SetActive(true);
         
         dialogueIsPlaying = true;

         playerMove.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

         playerMove.enabled = false;
         
         ContinueStory(director);
     }
     
     private IEnumerator ExitDialogueMode(PlayableDirector director)
     {
         yield return null;
         
         dialogueIsPlaying = false;

         dialoguePanel.SetActive(false);

         playerMove.enabled = true;

         if (director != null)
         {
             director.Resume();
         } 
     }
     
     public void ContinueStory(PlayableDirector director)
     {
         if (currentStory.canContinue)
         {
             dialogueText.text = currentStory.Continue();
         }
         else
         {
             StartCoroutine(ExitDialogueMode(director));
         }
     }
     
     public void Submit(InputAction.CallbackContext context)
     {
         if (context.performed && dialogueIsPlaying)
         {
             ContinueStory(mainDirector);
         }
     }
}