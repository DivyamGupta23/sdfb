using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;
using Unity.Collections;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class NetworkDataManager : NetworkBehaviour
{
    public Texture2D texture;
    public Renderer targetRenderer;

    [HideInInspector] public byte[] bytes;
    
    public string imageUrl = " ";

    public TMP_InputField inputField;
    public Button button;
    public Button downloadButton;
    public TextMeshProUGUI notificationText;


    public NetworkVariable<FixedString512Bytes> networkVariablePath = new NetworkVariable<FixedString512Bytes>(
        "old", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        targetRenderer = GetComponent<Renderer>();

        if (IsClient)
        {
            networkVariablePath.OnValueChanged += (oldVal, newVal) =>
            {
                Debug.Log($"old: {oldVal}, new: {newVal}");
            };  

        }

        if (IsHost)
        {
            inputField.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            button.onClick.AddListener(GetUrl);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void StartDownloadServerRpc() { StartDownloadClientRpc(); }


    [ClientRpc]
    public void StartDownloadClientRpc()
    {
        StartCoroutine(DownloadImage());
    }

    public void GetUrl()
    {
        imageUrl = inputField.text;
        networkVariablePath.Value = imageUrl.ToString();
        
        SetDownloadActiveServerRpc();
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetDownloadActiveServerRpc() { SetDownloadActiveClientRpc(); }
    [ClientRpc]
    public void SetDownloadActiveClientRpc()
    {
        notificationText.text = "URL has been set, Texture download available !";
        downloadButton.gameObject.SetActive(true);
        downloadButton.onClick.AddListener(SetTexture);
    }
    public IEnumerator DownloadImage()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(networkVariablePath.Value.ToString());
        yield return www.SendWebRequest();
        Debug.Log(networkVariablePath.Value.ToString());

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to download image: " + www.error);
            try { 
            Texture2D received_texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            texture = received_texture;

            }
            catch(System.Exception e) { 
                Debug.Log(e.Message);
                notificationText.text = e.Message;
            }
        }
        else
        {
            Texture2D received_texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            texture = received_texture;


        }
    }

    public void PickImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                byte[] textureByteArray = File.ReadAllBytes(path);
                bytes = textureByteArray;
            }
        });

        Debug.Log("Permission result: " + permission);
    }

    //[ClientRpc]
    //public void SetTextureFromBytesClientRpc(byte[] bytes)
    //{
    //    Texture2D texture = new Texture2D(2, 2);
    //    texture.LoadImage(bytes);
    //    targetRenderer.material.mainTexture = texture;
    //}

    //[ServerRpc]
    //public void SetTextureFromBytesServerRpc(byte[] bytes) { 
    //    SetTextureFromBytesClientRpc(bytes); 
    //}

    //public void ImagePut()
    //{
    //    PickImage();
    //    SetTextureFromBytesServerRpc(bytes);
    //}

    //public void SetImage()
    //{
    //    GetUrl(); 
    //}
    void SetTexture()
    {
        StartDownloadServerRpc();
        targetRenderer.material.mainTexture = texture;
    }



}
