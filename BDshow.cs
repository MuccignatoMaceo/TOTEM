using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BDshow : MonoBehaviour
{
    public string LevelToLoad; //On indique la scene à charger


    public void CloseBD()
    {  
        SceneManager.LoadScene(LevelToLoad); // On charge la scene qu'on a indiquer

    }

}
