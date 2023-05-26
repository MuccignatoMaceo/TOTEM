using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditTp : MonoBehaviour
{
    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Credits"); //On charge la scene Credits
    }
}
