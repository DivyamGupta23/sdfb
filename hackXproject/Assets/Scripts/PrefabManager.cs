using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] private GameObject quizPanelObject;
    [SerializeField] private QuizPanel quizPanel;
    [SerializeField] private GameObject ansPanel;
    [SerializeField] private int Quizindex;
    [SerializeField] private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        Quizindex = Random.Range(0, 9);
        //quizPanel = GameObject.Find("Menu Canvas").transform.GetChild(0).gameObject;
        //ansPanel = quizPanel.transform.GetChild(5).gameObject;
        //ansText = ansPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        //exitButton = quizPanel.transform.GetChild(6).gameObject.GetComponent<Button>();
        exitButton.onClick.AddListener(exitQuiz);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            quizPanel.StartQuizPanel(Quizindex);
            quizPanelObject.SetActive(true);

        }
    }
    public void exitQuiz()
    {
        quizPanelObject.SetActive(false);
        ansPanel.SetActive(false);
        //gameObject.SetActive(false);
        if(quizPanel.isAnswered) Destroy(gameObject, 0.5f);
    }



}
