using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Singleton;

    [SerializeField] ChatMessage chatMessagePrefab;
    [SerializeField] CanvasGroup chatContent;
    [SerializeField] TMP_InputField chatInput;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Button sendButton;
    [SerializeField] public Button openChatButton;
    public GameObject chatBox;
    
    public string playerName;

    void Awake() 
    { ChatManager.Singleton = this; }


    private void Start()
    {
        Application.targetFrameRate = 70;
        sendButton.onClick.AddListener(() => {
            SendChatMessage(chatInput.text, (nameInput.text != "" )? nameInput.text : "Annonymous");
            chatInput.text = "";
        });

        openChatButton.onClick.AddListener(() => {
            if (chatBox.activeSelf) chatBox.SetActive(false);
            else chatBox.SetActive(true);
        });
    }

    public void SendChatMessage(string _message, string _fromWho)
    { 
        if(string.IsNullOrWhiteSpace(_message)) return;

        string S = _message;
        SendChatMessageServerRpc(S, _fromWho); 
    }
   
    void AddMessage(string msg, string name)
    {
        ChatMessage CM = Instantiate(chatMessagePrefab, chatContent.transform);
        CM.SetName($"{name} : ");
        CM.SetText(msg);
    }

    [ServerRpc(RequireOwnership = false)]
    void SendChatMessageServerRpc(string message, string name)
    {
        ReceiveChatMessageClientRpc(message, name);
    }

    [ClientRpc]
    void ReceiveChatMessageClientRpc(string message, string name )
    {
        ChatManager.Singleton.AddMessage(message, name);
    }
}
