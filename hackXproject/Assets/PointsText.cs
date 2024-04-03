using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsText : MonoBehaviour
{
    public TMP_Text pointsText;
    public GameObject PoPpanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points: " + PlayerPrefs.GetInt("Points").ToString();
    }
    public void Done()
    {
        PoPpanel.SetActive(false);
    }
}
