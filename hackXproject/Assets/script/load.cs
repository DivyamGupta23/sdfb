using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
    public void loadWelcomeScreen()
    {
        SceneManager.LoadScene("web1");
    }
}