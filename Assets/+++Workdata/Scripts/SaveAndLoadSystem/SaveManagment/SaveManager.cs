using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Path = System.IO.Path;

public static class SaveManager
{
   private static string SaveFolderPath => Path.Combine(Application.persistentDataPath, "Savegames");

   private static string GetFilePath(string fileName) => Path.Combine(SaveFolderPath, $"{fileName}.json");

   public static bool TrySaveData<T>(string fileName, T data)
   {
      var path = GetFilePath(fileName);

      try
      {
         if (!Directory.Exists(SaveFolderPath))
         {
            Directory.CreateDirectory(SaveFolderPath);
         }

         if (File.Exists(path))
         {
            File.Delete(path);
         }

         using FileStream stream = File.Create(path);
         stream.Close();

         string jsonConvertedData = JsonConvert.SerializeObject(data, Formatting.Indented);
         File.WriteAllText(path, jsonConvertedData);
         return true;

      }
      catch(Exception e)
      {
         Debug.LogError($"Data cannot be saved due to: {e.Message} {e.StackTrace}");
         return false;
      }
   }

   public static bool TryLoadData<T>(string fileName, out T data)
   {
      var path = GetFilePath(fileName);

      data = default;

      if (!File.Exists(path))
      {
         Debug.LogWarning($"File cannot be loaded at \"{path}\".");
         return false;
      }

      try
      {
         data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
         return true;
      }
      catch (Exception e)
      {
        Debug.LogError($"Data cannot be loaded due to: {e.Message} {e.StackTrace}");
        return false;
      }
   }
}
