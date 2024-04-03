using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
public class TestLobby : MonoBehaviour
{
    private Lobby hostLobby;
    private float heartBeatTimer;
    async void Start()
    {
        await UnityServices.InitializeAsync();


        AuthenticationService.Instance.SignedIn += () => { Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId); };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateLobby()
    {
        try {
            string lobbyName = "TestLobby";
            int maxPlayers = 4;

            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);

            hostLobby = lobby;
            Debug.Log("Created lobby: " + lobbyName + " , " + maxPlayers);
        }
            catch(LobbyServiceException e) { Debug.Log(e); }
    }


    private void Update()
    {
        HandleLobbyHeartbeat();
    }
    async void HandleLobbyHeartbeat() 
    {   
        if(hostLobby != null)
        {
            heartBeatTimer -= Time.deltaTime;
            if(heartBeatTimer < 0f)
            {
                float maxHeartBeatTimer = 15;
                heartBeatTimer = maxHeartBeatTimer;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    public async void ListLobbies()
    {
        try
        {

            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots,"0",QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };


            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found" + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " ," + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e) { Debug.Log(e); }
    }

    public void JoinLobby() 
    {
        
    }

    
}
