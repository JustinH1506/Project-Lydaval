using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
   [SerializeField] private GameObject playerSelect;

   public void Select()
   {
      if(playerSelect.activeSelf)
         playerSelect.SetActive(false);
      else
         playerSelect.SetActive(true);
   }
}
