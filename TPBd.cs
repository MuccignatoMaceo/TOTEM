using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TPBd : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public string LevelToLoad;
    [SerializeField] private fondu Fondu; //On indique le script fondu
    [SerializeField] private GameObject bulle; // On indique ke gameoBject Bulle


    public void OnTriggerEnter2D(Collider2D collision)
    {
       bulle.SetActive(true); // On active la gameObject Bulle
        Invoke("Fondue", 2f);  //On appelle la fonction Fondue au bout de 2 secondes 
    } 

    public void Fondue()
    {
        Fondu.FadeOut = true; // on met le bool FadeOut dans fondu a vrai
        Invoke("BDshow", 3f); //On appelle la fonction BDshow au bout de 3 secondes 
    }


    public void BDshow()
    {

        SceneManager.LoadScene(LevelToLoad); //On charge la scene qu'on a indiqué 

    }

}
