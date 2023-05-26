using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    private static Inventaire instance = null;
    public static Inventaire Instance => instance;

    public List<GameObject> content = new List<GameObject>();

    public GameObject Sprite;
     
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void StoreObjectIntoInventory(GameObject objetToStore){
        content.Add(objetToStore);
    }
    
    private void Update()
    {
        if (ContainsObjects("Key"))
        {
            Sprite.SetActive(true);

        }
    }

    public bool ContainsObjects(string _name)
    {
        foreach(GameObject obj in content)
        {
            if(obj.name == _name)
            {

                return true;
            }

        }
        return false;
    }

}
