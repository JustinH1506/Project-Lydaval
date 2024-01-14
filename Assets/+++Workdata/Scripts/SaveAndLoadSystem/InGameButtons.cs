using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{
    public void StartGame()
    {
        GameStateManager.instance.StartNewGame();
    }
    
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        
        Destroy(GameStateManager.instance.gameObject);
        
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
