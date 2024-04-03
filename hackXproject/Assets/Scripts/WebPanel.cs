using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebPanel : MonoBehaviour
{
    public string urlString;
    public GameObject panel;
    // Start is called before the first frame update  
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            panel.SetActive(true);
            panel.GetComponent<WebLink>().url = urlString;
        }
    }

}
