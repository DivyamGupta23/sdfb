using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.IO;
// using Newtonsoft.Json;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Text;
using Michsky.MUIP;

public class ProductBackend : MonoBehaviour
{
    [SerializeField] private ButtonManager myButton;
    [SerializeField] private GameObject PoPpanel;
    [SerializeField] private int id;
    string data;
    [SerializeField] string endpoint = "https://twis.in/shop/wp-json/wc/v3/orders";

    [SerializeField] string consumer_key = "ck_51311e132ea3ab0b13c644bd7a50a058c03503ba";
    [SerializeField] string consumer_secret = "cs_2ea1fb2b4b9eb286fb720e1488c647a420440133";

    public Dictionary<string, object> formDataDict = new Dictionary<string, object>(){
        { "payment_method", "bacs" },
        { "payment_method_title", "Direct Bank Transfer" },
        { "set_paid", false },
        { "billing", new Dictionary<string, object>
            {
                { "first_name", "Divyam" },
                { "last_name", "Gupta" },
                { "address_1", "jankpuri" },
                { "address_2", "" },
                { "city", "New Delhi" },
                { "state", "Delhi" },
                { "postcode", "110058" },
                { "country", "India" },
                { "email", "divyam@example.com" },
                { "phone", "9122334455" }
            }
        },
        { "shipping", new Dictionary<string, object>
            {
                { "first_name", "Divyam" },
                { "last_name", "Gupta" },
                { "address_1", "Janakpuri" },
                { "address_2", "New delhi" },
                { "city", "New Delhi" },
                { "state", "Delhi" },
                { "postcode", "110058" },
                { "country", "India" }
            }
        },
        { "line_items", new List<Dictionary<string, object>>
            {
                // new Dictionary<string, object>
                // {
                //     { "product_id", 0 },
                //     { "quantity", 1 }
                // },

            }
        },
        { "shipping_lines", new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "method_id", "flat_rate" },
                    { "method_title", "Flat Rate" },
                    { "total", "10" }
                }
            }
        }
    };
    public void BackButton()
    {
        SceneManager.LoadScene("Assets/Scenes/Scene_lobby.unity");
    }
    public void Start()
    {
        SaveId();
        myButton.onClick.AddListener(SendData);
    }

    private void SaveId()
    {

        ((List<Dictionary<string, object>>)formDataDict["line_items"]).Add(new Dictionary<string, object>
    {
        { "product_id", id },
        { "quantity", 1}
    });

        data = JsonConvert.SerializeObject(formDataDict);

    }

    public void SendData()
    {
        WebRequest request = WebRequest.Create(endpoint);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(consumer_key + ":" + consumer_secret)));

        using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        {
            writer.Write(data);
        }

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.Created)
            {
                Debug.Log("Order created successfully");

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string response_content = reader.ReadToEnd();
                    Dictionary<string, object> response_data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response_content);
                    int order_id = Convert.ToInt32(response_data["id"]);
                    Debug.Log("Order ID: " + order_id);
                }
                PoPpanel.SetActive(true);
            }

            else
            {
                Console.WriteLine("Order creation failed with status code " + (int)response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    Debug.Log("Response content: " + reader.ReadToEnd());
                }
            }
        }
        catch (WebException ex)
        {
            Debug.Log("API request failed with error:");
            Debug.Log(ex.Message);



        }
        int Points = PlayerPrefs.GetInt("Points");
        Points -= 150;
        PlayerPrefs.SetInt("Points", Points);

    }
}
