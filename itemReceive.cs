using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemReceive : MonoBehaviour
{
    public Inventaire inventory;

    private void Start()
    {
        inventory = Inventaire.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("item"))
        {
            inventory.StoreObjectIntoInventory(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
