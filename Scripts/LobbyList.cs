using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LobbyList : MonoBehaviour
{
    public GameObject lobbyListPanel;
    public GameObject mainPanel;
    public Button leaveLobby;
    public Button fetchImages;
    public LobbyItem lobbyItemPrefab;
    public Transform lobbyItemParent;
    public GameObject connectingUI;
    private bool isRefreshing;
    private bool isJoining;
    public string code;
    public string lobbyID;
    public static LobbyList Instance;
    public GameObject buttons;
    public TMP_Text joinCodeText;

    public Renderer imagePanel1;
    public Renderer imagePanel2;
    public Renderer imagePanel3;    
    
    public ImageHandler image1;
    public ImageHandler image2;
    public ImageHandler image3;

    public Texture2D texture1;
    public Texture2D texture2;
    public Texture2D texture3;

    string url1;
    string url2;
    string url3;

    Lobby joinedLobby;
    private void Awake()
    {
        Instance = this;
        //this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        RefreshList();
    }


    

    public async void RefreshList()
    {
        if (isRefreshing) { return; }
        isRefreshing = true;

        try 
        {
            var options = new QueryLobbiesOptions();
            options.Count = 25;
            
            options.Filters = new List<QueryFilter>
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value : "0"),

                   new QueryFilter(
                    field: QueryFilter.FieldOptions.IsLocked,
                    op: QueryFilter.OpOptions.EQ,
                    value : "0")
            };

            var lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);

            foreach(Transform child in lobbyItemParent)
            {
                Destroy(child.gameObject);
            }
            foreach (Lobby lobby in lobbies.Results)
            {
                var lobbyinstance = Instantiate(lobbyItemPrefab, lobbyItemParent);
                lobbyinstance.Initialise(this, lobby);
            }
        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
            isRefreshing = false;
            throw;

        }   

        isRefreshing = false;
    }

    public async void JoinAsync(Lobby lobby)
    {
        if (isJoining) { return; }

        isJoining = true;
        lobbyListPanel.SetActive(false);
        connectingUI.SetActive(true);
        try
        {
            var joiningLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
            joinedLobby = joiningLobby;
            string joinCode = joiningLobby.Data["JoinCode"].Value;

            //Debug.Log(url);
            fetchImages.interactable = true;
            fetchImages.onClick.AddListener(FetchImages);
           // StartCoroutine(ImageDL.instance.DownloadImage(imagePanel1, url1, texture1));
            


            code = joinCode;
            lobbyID = lobby.Id;
            await TestRelay.instance.JoinRoom(joinCode);
            

        }
        catch(LobbyServiceException e)
        {
            Debug.Log(e);
            isJoining = false;
            throw;
        }

        
        isJoining = false;
  
    }

    public async void LeaveAsync()
    {
        try
        {
            //Ensure you sign-in before calling Authentication Instance
            //See IAuthenticationService interface
            string playerId = AuthenticationService.Instance.PlayerId;
            await LobbyService.Instance.RemovePlayerAsync(lobbyID, playerId);
            

            try
            {
                NetworkManager.Singleton.Shutdown();
            }
            catch(System.Exception e) { Debug.Log(e); }
            leaveLobby.interactable = false;
            fetchImages.interactable = false;
            mainPanel.SetActive(true);
            buttons.SetActive(true);
            joinCodeText.text = "";
            ChatManager.Singleton.openChatButton.interactable = false;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    public void FetchImages()
    {
        url1 = joinedLobby.Data["Url1"].Value;
        url2 = joinedLobby.Data["Url2"].Value;
        url3 = joinedLobby.Data["Url3"].Value;
        StartCoroutine(ImageDL.instance.DownloadImage(imagePanel1, url1, texture1,image1));
        StartCoroutine(ImageDL.instance.DownloadImage(imagePanel2, url2, texture2,image2));
        StartCoroutine(ImageDL.instance.DownloadImage(imagePanel3, url3, texture3,image3));
    }
}
