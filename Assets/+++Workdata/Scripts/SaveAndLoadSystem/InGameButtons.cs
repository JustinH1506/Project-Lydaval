using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.instance.CleanUp();
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.villageMusic);
        
        GameStateManager.instance.StartNewGame();
    }
    
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        
        Destroy(GameStateManager.instance.gameObject);
        
        AudioManager.instance.CleanUp();
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.mainMenuMusic);
        
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        GameStateManager.instance.SaveGame("SaveGame");
    }

    public void LoadGame()
    {
        GameStateManager.instance.LoadFromSave("SaveGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
