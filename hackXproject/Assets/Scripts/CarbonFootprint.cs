using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using TMPro;
public class CarbonFootprint : MonoBehaviour
{
    // [SerializeField]
    // private TMP_Text carbonText;
    // [System.Serializable]
    // private class CarbonFootprintResponse
    // {
    //     public float carbon;
    // }
    // async void Start()
    // {
    //     using (var client = new HttpClient())
    //     {
    //         var request = new HttpRequestMessage
    //         {
    //             Method = HttpMethod.Post,
    //             RequestUri = new Uri("https://tracker-for-carbon-footprint-api.p.rapidapi.com/traditionalHydro"),
    //             Headers =
    //             {
    //                 { "X-RapidAPI-Key", "11ef0dd765msh774ad121ae5ed30p1cf493jsnc2703e49c028" },
    //                 { "X-RapidAPI-Host", "tracker-for-carbon-footprint-api.p.rapidapi.com" },
    //             },
    //             Content = new FormUrlEncodedContent(new Dictionary<string, string>
    //             {
    //                 { "consumption", "500" },
    //                 { "location", "OtherCountry" },
    //             }),
    //         };
    //         using (var response = await client.SendAsync(request))
    //         {
    //             response.EnsureSuccessStatusCode();
    //             var body = await response.Content.ReadAsStringAsync();
    //             // Debug.Log(body);
    //             CarbonFootprintResponse result = JsonUtility.FromJson<CarbonFootprintResponse>(body);
    //             carbonText.text = result.carbon.ToString() + " kg co2";
    //             Debug.Log("Carbon footprint: " + result.carbon);

    //         }
    //     }
    // }
}
