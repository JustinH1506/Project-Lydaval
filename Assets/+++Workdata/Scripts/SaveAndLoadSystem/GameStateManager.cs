using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
   public static GameStateManager instance;

   public GameData data = new GameData();
   
   private void Awake()
   {
      if(instance != null)
      {
         Destroy(gameObject);
         return;
      }
      
      instance = this;
     
      DontDestroyOnLoad(this);
   }

   public void StartNewGame()
   {
      data = new GameData();

      SceneManager.LoadScene(1);
   }
   
   public void LoadFromSave(string saveName)
   {
      if (!SaveManager.TryLoadData<GameData>(saveName, out var loadedData))
         return;
      
      data = loadedData;

      SceneManager.LoadScene(1);
   }

   public void SaveGame(string saveName)
   {
      SaveManager.TrySaveData(saveName, data);
   }
}
