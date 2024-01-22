using System;
using UnityEngine;
using UnityEngine.UI;

public class UiButtons : MonoBehaviour
{
    public GameObject buttonFunction1, buttonFunction2, buttonFunction3,buttonFunction4;

    private float alphaThreshold = 0.1f;

    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

    public void ActivateObject()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        buttonFunction1.SetActive(true);
        
        buttonFunction2.SetActive(false);
        
        buttonFunction3.SetActive(false);
        
        buttonFunction4.SetActive(false);
    }

    public void DeactivateTutorial()
    {
        buttonFunction1.SetActive(false);
    }
}
