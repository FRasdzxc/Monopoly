using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    public GameObject panel;
    public GameObject p3;
    public GameObject p3Score;
    public GameObject p4;
    public GameObject p4Score;
    public int playerNum;
    void Start()
    {
        playerNum = PlayerPrefs.GetInt("playerNum");
        if(playerNum == 2)
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 250);
            p3.SetActive(false);
            p3Score.SetActive(false);
            p4.SetActive(false);
            p4Score.SetActive(false);
        }else if(playerNum == 3)
        {
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 375);
            p4.SetActive(false);
            p4Score.SetActive(false);
        }
    }
}
