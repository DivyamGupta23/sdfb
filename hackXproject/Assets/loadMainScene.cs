using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMainScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadScene("Playground");
    }
}
