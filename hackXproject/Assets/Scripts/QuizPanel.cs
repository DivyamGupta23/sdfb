using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.MUIP;
using TMPro;
using System;
public class QuizPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ButtonManager buttonA;
    [SerializeField] private ButtonManager buttonB;
    [SerializeField] private ButtonManager buttonC;
    [SerializeField] private ButtonManager buttonD;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text answervalidityText;
    [SerializeField] private TMP_Text correctAnsText;
    // [SerializeField] private TMP_Text Text;
    [SerializeField] private int QuizIndex;
    [SerializeField] private int points;
    [SerializeField] private GameObject answerPanel;
    public bool isAnswered = false;

    Dictionary<string, Tuple<string, string, string, string, string>> web3Quiz = new Dictionary<string, Tuple<string, string, string, string, string>>()
{
    {"What is Web3 technology primarily based on?", Tuple.Create("Blockchain", "Cloud", "AI", "IoT", "A")},
    {"What is the main purpose of blockchain in Web3?", Tuple.Create("Secure Transactions", "Store Photos", "Play Games", "Send Emails", "A")},
    {"What are self-executing contracts on Web3 called?", Tuple.Create("Smart Contracts", "Digital Money", "Online Chats", "Virtual Goods", "A")},
    {"What's the key goal of Web3 technology?", Tuple.Create("Decentralization", "Centralization", "Speed", "Simplicity", "A")},
    {"What keeps Web3 transactions safe from hackers?", Tuple.Create("Cryptography", "Open Source", "Magic", "Good Luck", "A")},
    {"Where do Web3 platforms store data in a decentralized way?", Tuple.Create("IPFS", "Google Drive", "Dropbox", "USB Drive", "A")},
    {"What's the common way to agree on transactions in Web3?", Tuple.Create("Proof of Stake", "Handshake", "Coin Toss", "Paperwork", "A")},
    {"What type of tokens represent unique items in Web3?", Tuple.Create("NFTs", "Bitcoin", "Dollars", "Stamps", "A")},
    {"What's the idea of having control over your online identity in Web3?", Tuple.Create("Self-Sovereign ID", "Government ID", "Social Media Profile", "No Identity", "A")}
};





    public void StartQuizPanel(int randomIndex)
    {
        string randomQuestion = GetRandomQuestion(web3Quiz, randomIndex);
        // Display the question and answer options to the player
        DisplayQuestion(randomQuestion, web3Quiz[randomQuestion]);
    }

    private string GetRandomQuestion(Dictionary<string, Tuple<string, string, string, string, string>> quiz, int randomIndex)
    {
        List<string> keysList = new List<string>(quiz.Keys);
        // Get a random index from the list of keys
        // Return the question at the random index
        return keysList[randomIndex];
    }
    private void DisplayQuestion(string question, Tuple<string, string, string, string, string> answerOptions)
    {
        // Display the question to the player
        //     Debug.Log(question);

        //     // Display the answer options to the player
        //     Debug.Log("A) " + answerOptions.Item1);
        //     Debug.Log("B) " + answerOptions.Item2);
        //     Debug.Log("C) " + answerOptions.Item3);
        //     Debug.Log("D) " + answerOptions.Item4);
        //     Debug.Log("Answer:  " + answerOptions.Item5);
        questionText.text = question;
        buttonA.SetText(answerOptions.Item1);
        buttonB.SetText(answerOptions.Item2);
        buttonC.SetText(answerOptions.Item3);
        buttonD.SetText(answerOptions.Item4);

        if (answerOptions.Item5 == "A")
        {
            buttonA.onClick.AddListener(CorrectAns);
            buttonB.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonC.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonD.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
        }
        else if (answerOptions.Item5 == "B")
        {
            buttonB.onClick.AddListener(CorrectAns);
            buttonC.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonD.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonA.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
        }
        else if (answerOptions.Item5 == "C")
        {
            buttonC.onClick.AddListener(CorrectAns);
            buttonA.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonB.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonD.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
        }
        else if (answerOptions.Item5 == "D")
        {
            buttonD.onClick.AddListener(CorrectAns);
            buttonA.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonB.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
            buttonC.onClick.AddListener(delegate { WrongAns(answerOptions.Item5); });
        }

        // }
    }
    public void CorrectAns()
    {
        isAnswered = true;
        points = PlayerPrefs.GetInt("Points", 0);
        points += 25;
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.Save();
 
        answerPanel.SetActive(true);
        answervalidityText.text = "Correct";
        correctAnsText.text = "+ Added Points!";


    }
    public void WrongAns(string CorrectAnswer)
    {
        isAnswered = true;
        points = PlayerPrefs.GetInt("Points", 0);
        points -= 10;
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.Save();
  
        answerPanel.SetActive(true);
        answervalidityText.text = "Wrong";
        correctAnsText.text = "Correct answer: " + CorrectAnswer;
    }

}
