using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
   public static GameStateManager instance;

   public GameData data = new GameData();

   public Stats heroStats;
   
   public Stats healerStats;
   
   public Stats tankStats;
   
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

      data.heroStatData = heroStats.data;

      data.healerStatData = healerStats.data;

      data.tankStatData = tankStats.data;

      heroStats = null;

      healerStats = null;

      tankStats = null;

      SceneManager.LoadScene(1);
   }
   
   public void LoadFromSave(string saveName)
   {
      if (!SaveManager.TryLoadData<GameData>(saveName, out var loadedData))
         return;
      
      data = loadedData;
      
      heroStats = null;

      healerStats = null;

      tankStats = null;

      SceneManager.LoadScene(1);
   }

   public void SaveGame(string saveName)
   {
      SaveManager.TrySaveData(saveName, data);
   }
}
