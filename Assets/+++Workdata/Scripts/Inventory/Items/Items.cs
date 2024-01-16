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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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

    public void Update()
    {
        if (transform.position.y < player.transform.position.y)
            sr.sortingOrder = 10;
        else
        {
            sr.sortingOrder = -10;
        }
    }


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