using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI nameText;

    public void SetText(string str)
    { messageText.text = str; }
    public void SetName(string str)
    { nameText.text = str; }
}
