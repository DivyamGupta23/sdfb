using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDL : MonoBehaviour
{
    public Texture2D savedTexture_renderer1;
    public static ImageDL instance;
    public string imageUrl = "";
    public Renderer targetRenderer;
 
    private void Awake()
    {
        instance = this;
    }
    //public void StartDownload()
    //{
    //    StartCoroutine(DownloadImage());
    //}
    public void SetTexture(Renderer targetRenderer, Texture2D savedTexture_renderer1)
    {
        targetRenderer.material.mainTexture = savedTexture_renderer1;
    }
    public IEnumerator DownloadImage(Renderer targetRenderer, string imageUrl, Texture2D texture, ImageHandler imagePanel)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();
        Debug.Log(imageUrl);
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download image: " + www.error);
        }
        else
        {
            texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            imagePanel.pickedImage = texture;
            SetTexture(targetRenderer, texture);
            //targetRenderer.material.mainTexture = texture;
        }
    }
}
