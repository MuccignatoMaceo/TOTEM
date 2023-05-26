using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CléPorte : MonoBehaviour
{
    public GameObject porte;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            porte.SetActive(false);
        }
    }
}
