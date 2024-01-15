using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public enum ItemType
{
   HealthItem,
   Weapon,
   Armor,
}
public class Items : MonoBehaviour
{
   [SerializeField] private string uniqueGuid;
   
   [System.Serializable]
   public class Data
   {
      public string newName;

      public ItemType itemType;

      public int healAmount, atk, def;

      public int amount;
   }

   [System.Serializable]
   public class PositionData
   {
      public SaveableVector3 position;
      public bool playerGotIt;
   }

   [SerializeField] private Inventory inventory;

   public Data data;

   public PositionData positionData;

   private void Start()
   {
      PositionData loadedData = GameStateManager.instance.data.GetItemPosition(uniqueGuid);

      if (loadedData != null)
      {
         positionData = loadedData;

         transform.position = positionData.position;
            
         if(positionData.playerGotIt)
            gameObject.SetActive(false);
      }
      else
      {
         GameStateManager.instance.data.SpawnItem(uniqueGuid, positionData);
         positionData.position = transform.position;
      }
        
      positionData.position = transform.position;
   }

   private void OnValidate()
   {
      if (string.IsNullOrEmpty(gameObject.scene.name))
      {
         uniqueGuid = "";
      }
      else if (string.IsNullOrEmpty(uniqueGuid))
      {
         uniqueGuid = System.Guid.NewGuid().ToString();
      }
   }
   
   public void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         inventory.AddItemToList(data);

         positionData.playerGotIt = true;
      
         gameObject.SetActive(false);
      }
   }
}
