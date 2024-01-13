using System.Collections;
using UnityEngine;

public enum ItemType
{
   HealthItem,
   Weapon,
   Armor,
}
public class Items : MonoBehaviour
{
   [System.Serializable]
   public class Data
   {
      public string newName;

      public ItemType itemType;

      public int healAmount, atk, def;

      public int amount;
   }

   [SerializeField] private Inventory inventory;

   public Data data;

   public void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         StartCoroutine(WaitForBeforeDestruction());
      }
   }

   private IEnumerator WaitForBeforeDestruction()
   {
      inventory.AddItemToList(data);
      
      yield return null;
      
      gameObject.SetActive(false);
   }
}
