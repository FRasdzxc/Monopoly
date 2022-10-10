using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] GameObject answer;
    [SerializeField] GameObject question;
    [SerializeField] GameObject btn_ShowAnswer;
    [SerializeField] GameObject btn_Correct;
    [SerializeField] GameObject btn_Wrong;
    [SerializeField] GameObject messageBox;
    [SerializeField] GameObject settingPanel;
    [SerializeField] Dropdown winningScore;
    public AudioSource source;
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioClip buttonClickClip;
    public Text turnTemp;
    public Text message;
    public static Text turn;
    public static bool checkedAnswer = false;
    bool openSelector = false;
    int score;
    void Start()
    {
        openSelector = false;
        turn = turnTemp;
    }
    void Update()
    {
        if(Move.reset == true)
        {
            question.SetActive(true);
            answer.SetActive(false);
            btn_ShowAnswer.SetActive(true);
            btn_Correct.SetActive(false);
            btn_Wrong.SetActive(false);
            messageBox.SetActive(false);
            Move.reset = false;
        }
        if(openSelector == true)
        {
            if (winningScore.value == 0)
            {
                score = 10;
            }
            if (winningScore.value == 1)
            {
                score = 20;
            }
            if (winningScore.value == 2)
            {
                score = 30;
            }
        }
    }
    public void switchScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }
    public void setPlayerNum(int num)
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("winningScore", score);
        PlayerPrefs.SetInt("playerNum", num);
    }

    public void setActice(bool x)
    {
        obj.SetActive(x);
        openSelector = true;
    }
    public void openSetting()
    {
        settingPanel.SetActive(true);
    }
    public void closeSetting()
    {
        settingPanel.SetActive(false);
    }
    public async void Correct()
    {
        if (Move.turn == 0)
        {
            LoadExcel.p1Score++;

        }else if(Move.turn == 1)
        {
            LoadExcel.p2Score++;
        }else if(Move.turn == 2)
        {
            LoadExcel.p3Score++;
        }else if(Move.turn == 3)
        {
            LoadExcel.p4Score++;
        }
        message.text = "Correct! +1 Score.";
        answer.SetActive(false);
        btn_Correct.SetActive(false);
        btn_Wrong.SetActive(false);
        messageBox.SetActive(true);
        await Task.Delay(2000);
        checkedAnswer = true;
        Move.answered = true;
    }
    public void Wrong()
    {
        answer.SetActive(false);
        btn_Correct.SetActive(false);
        btn_Wrong.SetActive(false);
        messageBox.SetActive(true);
        message.text = "Ops! You wrong.";
        checkedAnswer = true;
        Move.answered = true;
    }
    public void checkAnser()
    {
        question.SetActive(false);
        answer.SetActive(true);
        btn_ShowAnswer.SetActive(false);
        btn_Correct.SetActive(true);
        btn_Wrong.SetActive(true);
    }
    public static void changeTurnText(int turns)
    {
        if(turns == 0)
        {
            turn.text = "Red";
        }
        if(turns == 1)
        {
            turn.text = "Yellow";
        }
        if(turns == 2)
        {
            turn.text = "Blue";
        }
        if(turns == 3)
        {
            turn.text = "Green";
        }
    }
    public void playCorrectSound()
    {
        source.PlayOneShot(correctClip);
    }

    public void playWrongSound()
    {
        source.PlayOneShot(wrongClip);
    }

    public void playButtonClickSound()
    {
        source.PlayOneShot(buttonClickClip);
    }
}
