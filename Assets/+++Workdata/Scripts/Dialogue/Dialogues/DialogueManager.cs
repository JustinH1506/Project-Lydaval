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

        dialogueControllMap.Dialogue.Submit.performed += Submit;
        dialogueControllMap.Dialogue.Submit.canceled += Submit;
    }

    /// <summary>
    /// We disable dialogueControllMap and deSubscribe it to Submit by performed and canceled. 
    /// </summary>
    private void OnDisable()
    {
        dialogueControllMap.Disable();
        
        dialogueControllMap.Dialogue.Submit.performed -= Submit;
        dialogueControllMap.Dialogue.Submit.canceled -= Submit;
    }

    /// <summary>
    /// We start the dialogue with the given story adn the playable director. 
    /// </summary>
    /// <param name="inkJSON"></param>
    /// <param name="director"></param>
     public void EnterDialogueMode(TextAsset inkJSON, PlayableDirector director)
     {
         mainDirector = director;
         
         if(director != null)
         {
             director.Pause();
         }         
         
         currentStory = new Story(inkJSON.text);

         dialoguePanel.SetActive(true);
         
         dialogueIsPlaying = true;

         playerMove.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

         playerMove.enabled = false;
         
         ContinueStory(director);
     }
     
    /// <summary>
    /// We deactivate the panel and Resume the director if its not null.
    /// </summary>
    /// <param name="director"></param>
    /// <returns></returns>
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
     
    /// <summary>
    /// If the story can continue we call continue else we start the ExitDialogueMode
    /// </summary>
    /// <param name="director"></param>
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
     
    /// <summary>
    /// If we press while Dialogue is playing we start ContinueStory. 
    /// </summary>
    /// <param name="context"></param>
     public void Submit(InputAction.CallbackContext context)
     {
         if (context.performed && dialogueIsPlaying)
         {
             ContinueStory(mainDirector);
         }
     }
}