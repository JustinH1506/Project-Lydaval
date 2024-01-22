using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    /// <summary>
    /// Play a sound, CleanUp the Audio, start new Music and Call StartNewGame.
    /// </summary>
    public void StartGame()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        AudioManager.instance.CleanUp();
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.villageMusic);
        
        GameStateManager.instance.StartNewGame();
    }
    
    /// <summary>
    /// Play a sound, cleanUp Audio, set Time to 1, Destroy the GameStateManager, start new Music, load Scene 0.
    /// </summary>
    public void BackToMainMenu()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        AudioManager.instance.CleanUp();
        
        Time.timeScale = 1;
        
        Destroy(GameStateManager.instance.gameObject);
        
        AudioManager.instance.InitializeMusic(FmodEvents.instance.mainMenuMusic);
        
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Play sound, call SaveGame.
    /// </summary>
    public void SaveGame()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        GameStateManager.instance.SaveGame("SaveGame");
    }

    /// <summary>
    /// PlaySound, call LoadFromSave.
    /// </summary>
    public void LoadGame()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);
        
        GameStateManager.instance.LoadFromSave("SaveGame");
    }

    /// <summary>
    /// Play sound, Quit the Game.
    /// </summary>
    public void Quit()
    {
        AudioManager.instance.PlayOneShot(FmodEvents.instance.buttonSound, transform.position);

        Application.Quit();
    }
}
