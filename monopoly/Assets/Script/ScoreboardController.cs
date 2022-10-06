using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{
    public Text winningScoreText;
    public GameObject panel;
    public GameObject p3;
    public GameObject p4;
    public Slider p1Slider;
    public Slider p2Slider;
    public Slider p3Slider;
    public Slider p4Slider;
    public int playerNum;
    int winningScore;
    void Start()
    {
        winningScore = PlayerPrefs.GetInt("winningScore");
        if (winningScore == 10)
        {
            winningScoreText.text = "Reach 10 scores to win";
        }
        if (winningScore == 20)
        {
            winningScoreText.text = "Reach 20 scores to win";
        }
        if (winningScore == 30)
        {
            winningScoreText.text = "Reach 30 scores to win";
        }
        playerNum = PlayerPrefs.GetInt("playerNum");
        if(playerNum == 2)
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 250);
            p3.SetActive(false);
            p4.SetActive(false);

        }else if(playerNum == 3)
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 375);
            p4.SetActive(false);
        }
        p1Slider.maxValue = winningScore;
        p2Slider.maxValue = winningScore;
        p3Slider.maxValue = winningScore;
        p4Slider.maxValue = winningScore;
    }
    void Update()
    {
        if(playerNum == 2)
        {
            p1Slider.value = LoadExcel.p1Score;
            p2Slider.value = LoadExcel.p2Score;
        }
        if(playerNum == 3)
        {
            p1Slider.value = LoadExcel.p1Score;
            p2Slider.value = LoadExcel.p2Score;
            p3Slider.value = LoadExcel.p3Score;
        }
        if(playerNum == 4)
        {
            p1Slider.value = LoadExcel.p1Score;
            p2Slider.value = LoadExcel.p2Score;
            p3Slider.value = LoadExcel.p3Score;
            p4Slider.value = LoadExcel.p4Score;
        }

    }
}
