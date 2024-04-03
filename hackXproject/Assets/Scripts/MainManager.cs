using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour
{
    public Spawnner spawnner;
    public string scenename;
    public int points;
    [SerializeField]
    private TMP_Text pointsText;
    public GameObject enetrRoompanel;
    public static MainManager Instance;
    void Start()
    {
        Instance = this;
        PlayerPrefs.SetInt("Points", 0);
    }

    // Update is called once per frame
    void Update()
    {
        spawnner.spawnnerSpawn();
        pointsText.text = "EcoCoins : " + PlayerPrefs.GetInt("Points").ToString();
    }
    public void loadScene()
    {
        SceneManager.LoadScene(scenename);
    }
    public void Back()
    {
        enetrRoompanel.SetActive(false);
    }
}
