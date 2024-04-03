using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using TMPro;

public class WeatherController : MonoBehaviour
{
    // Start is called before the first frame update
    // API key and location for the weather dat

    // Unity game object to display the weather icon
    public GameObject weatherIcon;

    [SerializeField]
    private TMP_Text tempText;
    [SerializeField]
    private TMP_Text humidText;
    [SerializeField]
    private TMP_Text visibText;

    // Start is called before the first frame update
    private string url = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/delhi?unitGroup=metric&key=VM3X3YHLZXGRC5ZUFG9SPHGCB&contentType=json";

    void Start()
    {
        //using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        //{
        //    yield return webRequest.SendWebRequest();

        //    if (webRequest.result == UnityWebRequest.Result.Success)
        //    {
        //        string jsonResponse = webRequest.downloadHandler.text;
        //        JObject data = JObject.Parse(jsonResponse);

        //        // Access the weather details from the JSON response
        //        string weather = (string)data["days"][0]["temp"];
        //        string humidity = (string)data["days"][0]["humidity"];
        //        string visibility = (string)data["days"][0]["visibility"];
        //        tempText.text = "temprature: " + weather;
        //        visibText.text = "visibility: " + visibility + "KM";
        //        humidText.text = "humidity: " + humidity + "%";

        //        Debug.Log($"The current weather in Delhi is: {weather}");
        //    }
        //    else
        //    {
        //        Debug.Log($"Error: {webRequest.error}");
        //    }
        //}
        //Debug.Log("NIGGGGGGGGGGA");
    }
}

