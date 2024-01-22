using UnityEngine;

public class PlayerSkills : MonoBehaviour
{ 
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject deselect;

    [SerializeField] private BattleSystem _battleSystem;

    /// <summary>
    /// If select is active we set select off and inBetweenText on;
    /// Else we set select on and inBetweenText off. 
    /// </summary>
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

    /// <summary>
    /// Deselect is not active. 
    /// </summary>
   public void Deselect()
   {
      deselect.SetActive(false);
   }

    /// <summary>
    /// attacking is true and inBetweenText is deactivated. 
    /// </summary>
   public void Attacking()
   {
       _battleSystem.attacking = true;

       _battleSystem.inBetweenText.gameObject.SetActive(false);
   }

    /// <summary>
    /// attacking is false and inBetweenText is deactivated. 
    /// </summary>
   public void NotAttacking()
   {
       _battleSystem.attacking = false;
       
       _battleSystem.inBetweenText.gameObject.SetActive(false);
   }
}
