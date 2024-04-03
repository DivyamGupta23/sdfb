using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using TMPro;
using System.Threading.Tasks;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class TestRelay : MonoBehaviour
{
    public Button leaveLobby;
    public TMP_Text joinCodeText;
    public TMP_InputField joinCodeInput;
    public TMP_InputField roomNameInput;
    public GameObject buttons;
    public GameObject createRoomUI;
    public GameObject mainPanel;
    string lobbyId;
    public GameObject connectingUI;
    public static TestRelay instance;
    public UnityTransport _transport;
    private const int MaxPlayers = 5;
    public string joinCode;
    public ImageHandler imagePanel1;
    public ImageHandler imagePanel2;
    public ImageHandler imagePanel3;
    public UIManager uIManager;
    // Start is called before the first frame update
    private async void  Awake()
    {
        
        leaveLobby.interactable = false;
        instance = this;
       
            

        buttons.SetActive(false);

        await Authenticate();
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        connectingUI.SetActive(false);
        buttons.SetActive(true);
        //uIManager.SlideIn();
    }

    private void OnClientConnected(ulong obj)
    {
        if (obj == NetworkManager.Singleton.LocalClientId)
        {
            Debug.Log("player joined : " + obj);
            connectingUI.SetActive(false);
            mainPanel.SetActive(false);
            ChatManager.Singleton.openChatButton.interactable = true;
      
        }
    }
    public static async Task Authenticate() 
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        
    }

    public async void CreateRoom()
    {

        try {
            buttons.SetActive(false);
            createRoomUI.SetActive(false);
            connectingUI.SetActive(true);
 

            Allocation a = await RelayService.Instance.CreateAllocationAsync(MaxPlayers, "asia-south1");
            joinCodeText.text = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
            joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
            RelayServerData relayServerData = new RelayServerData(a, "dtls");


            //_transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);
            _transport.SetRelayServerData(relayServerData);

            try 
            {
                var createLobbyOptions = new CreateLobbyOptions();
                createLobbyOptions.IsPrivate = false;
                createLobbyOptions.Data = new Dictionary<string, DataObject>()
                {
                    {
                        "JoinCode", new DataObject(
                            visibility: DataObject.VisibilityOptions.Member,
                            value: joinCode)
                    },

                    {
                        "Url1", new DataObject(
                            visibility: DataObject.VisibilityOptions.Member,
                            value: imagePanel1.uploadedUrl)
                    },
                    {
                        "Url2", new DataObject(
                            visibility: DataObject.VisibilityOptions.Member,
                            value: imagePanel2.uploadedUrl)
                    },
                    {
                        "Url3", new DataObject(
                            visibility: DataObject.VisibilityOptions.Member,
                            value: imagePanel3.uploadedUrl)
                    }
                };

                Lobby lobby = await Lobbies.Instance.CreateLobbyAsync(roomNameInput.text, MaxPlayers, createLobbyOptions);
                lobbyId = lobby.Id;
                LobbyList.Instance.lobbyID = lobby.Id;
                StartCoroutine(HeartbeatCoroutine(15));
            }
            catch (LobbyServiceException e) {
                Debug.Log(e );
                throw;
            }


            NetworkManager.Singleton.StartHost();
            connectingUI.SetActive(false);
            leaveLobby.interactable = true;
        }
        catch (RelayServiceException e) { Debug.Log(e); }
    }

    private IEnumerator HeartbeatCoroutine(float delay)
    {
        var delaySec = new WaitForSeconds(delay);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delaySec;
        }
    }

    public async Task JoinRoom(string joincode)
    {
        try {
            
            buttons.SetActive(false);
            //joincode = joinCodeInput.text;
            JoinAllocation a = await RelayService.Instance.JoinAllocationAsync(joincode);
            joinCodeText.text = joincode;
            RelayServerData relayServerData = new RelayServerData(a, "dtls");

            _transport.SetRelayServerData(relayServerData);

            // _transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);

            NetworkManager.Singleton.StartClient();
           
            leaveLobby.interactable = true;

        }
        catch (RelayServiceException e) { Debug.Log(e); }
        }




}

