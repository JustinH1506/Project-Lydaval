using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        AudioManager.instance.CleanUp();
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.villageMusic);
        
        GameStateManager.instance.StartNewGame();
    }
    
    public void BackToMainMenu()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        AudioManager.instance.CleanUp();
        
        Time.timeScale = 1;
        
        Destroy(GameStateManager.instance.gameObject);
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.mainMenuMusic);
        
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        GameStateManager.instance.SaveGame("SaveGame");
    }

    public void LoadGame()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        GameStateManager.instance.LoadFromSave("SaveGame");
    }

    public void Quit()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);

        Application.Quit();
    }
}
