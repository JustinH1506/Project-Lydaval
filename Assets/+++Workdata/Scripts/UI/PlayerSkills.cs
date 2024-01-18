using UnityEngine;

public class PlayerSkills : MonoBehaviour
{ 
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject deselect;

    [SerializeField] private BattleSystem _battleSystem;

   public void Select()
   {
      if(select.activeSelf)
      {
          select.SetActive(false);
          
          _battleSystem.inBetweenText.gameObject.SetActive(true);
      }      
      else
      {
          select.SetActive(true);
          
          _battleSystem.inBetweenText.gameObject.SetActive(false);

      }   
   }

   public void Deselect()
   {
      deselect.SetActive(false);
   }

   public void Attacking()
   {
       _battleSystem.attacking = true;

       _battleSystem.inBetweenText.gameObject.SetActive(false);
   }

   public void NotAttacking()
   {
       _battleSystem.attacking = false;
       
       _battleSystem.inBetweenText.gameObject.SetActive(false);
   }
}
