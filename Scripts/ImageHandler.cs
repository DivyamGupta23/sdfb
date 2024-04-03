using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    [SerializeField]private string apiKey = "YOUR_API_KEY";

    // Replace with your image path or texture
    public string imagePath = "";
    public string uploadedUrl = "";
    public Renderer targetRenderer;
    public GameObject imagePanelUI;
    public RawImage rawImage;
    public Texture2D pickedImage;
    public GameObject createButton;
    public void StartUpload(bool trigger)
    {
        PickImageFromGallery(trigger);
        
    }


    public void PickImageFromGallery(bool trigger,int maxSize = 1024)
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Create Texture from selected image
                pickedImage = NativeGallery.LoadImageAtPath(path, maxSize);
                imagePath = path;
                StartCoroutine(UploadImage());
                ImageDL.instance.SetTexture(targetRenderer, pickedImage);
                createButton.SetActive(trigger);
                if (pickedImage == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
            }
        });
    }

    IEnumerator UploadImage()
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
      //  byte[] imageBytes = pickedImage.GetRawTextureData();

        string uploadUrl = $"https://api.imgbb.com/1/upload?expiration=3600&key={apiKey}";

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageBytes, "myTexture.png", "image/png");

        using (UnityWebRequest www = UnityWebRequest.Post(uploadUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = www.downloadHandler.text;
                Debug.Log("Response: " + jsonResponse);

                // Parse the JSON response to get the image URL
                string imageUrl = JsonUtility.FromJson<ImgBBResponse>(jsonResponse).data.url;
                uploadedUrl = imageUrl;
                Debug.Log("Image URL: " + imageUrl);
            }
            else
            {
                Debug.LogError("Error uploading image: " + www.error);
            }
        }
    }



}

[Serializable]
public class ImgBBResponse
{
    public ImgBBData data;
}

[Serializable]
public class ImgBBData
{
    public string url;
}
