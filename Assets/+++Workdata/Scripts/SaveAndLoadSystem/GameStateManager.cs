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
   
   /// <summary>
   /// Make this object to an instance. 
   /// </summary>
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

   /// <summary>
   /// Get new GameData, save hero, healer and tank Stats in certain data and set them null.
   /// </summary>
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
   
   /// <summary>
   /// If we dont have data we return else our data is loadedData, we make the stats null and load scene 1.
   /// </summary>
   /// <param name="saveName"></param>
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

   /// <summary>
   /// We call TrySaveGame. 
   /// </summary>
   /// <param name="saveName"></param>
   public void SaveGame(string saveName)
   {
      SaveManager.TrySaveData(saveName, data);
   }
}
