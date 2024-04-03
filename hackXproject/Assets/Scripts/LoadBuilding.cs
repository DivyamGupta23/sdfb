using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    public string Bulding;
    public GameObject enetrRoompanel;
    public TMP_Text text;
 
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            text.text = "Eneter Building: " + Bulding + " ?";
            enetrRoompanel.SetActive(true);
            MainManager.Instance.scenename = sceneName;
        }
    }
}


// Path: Assets\Scripts\MainManager.cs
