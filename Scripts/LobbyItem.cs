using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;

public class LobbyItem : MonoBehaviour
{
    public TMP_Text lobbyNameText;
    public TMP_Text lobbyPlayerstext;
    
    private Lobby lobby;
    private LobbyList lobbyList;

    public void Initialise(LobbyList list ,Lobby lobby)
    {
        this.lobbyList = list;
        this.lobby = lobby;

        lobbyNameText.text = lobby.Name;
        lobbyPlayerstext.text = $"{lobby.Players.Count} / {lobby.MaxPlayers}";
    }

    public void Join()
    {
        lobbyList.JoinAsync(lobby);
    }  
}
