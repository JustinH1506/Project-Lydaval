using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Items : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string newName;

        public int healAmount;

        public int amount;

        public bool playerGotIt;
    }

    [SerializeField] private Inventory inventory;

    [SerializeField] private GameObject player;

    private SpriteRenderer sr;

    public Data data;

    /// <summary>
    /// Set sr to SpriteRenderer. 
    /// </summary>
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// If data name is equal to name of setItemActiveList we set data to this data.
    /// If playerGotIt is true we set gameObject false. 
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < inventory.data.setItemActiveList.Count; i++)
        {
            if (data.newName == inventory.data.setItemActiveList[i].newName)
            {
                data = inventory.data.setItemActiveList[i];
            }
        }

        if (data.playerGotIt)
            gameObject.SetActive(false);
    }
    
    /// <summary>
    /// If player trigger this we add data to the list set playerGotIt to true and set gameObject to false. 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.data.setItemActiveList.Add(data);

            inventory.AddItemToList(data);

            data.playerGotIt = true;

            gameObject.SetActive(false);
        }
    }
}