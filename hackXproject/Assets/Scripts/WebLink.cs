using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLink : MonoBehaviour
{
    // Start is called before the first frame update
    public string url;

    public void Link()
    {
        Application.OpenURL(url);
    }
    public void Back()
    {
        gameObject.SetActive(false);
    }
}
