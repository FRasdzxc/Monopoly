using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadExcel : MonoBehaviour
{
    string path;
    public Question blankQuestion;
    public List<Question> questionDatabase;
    public Text questionText;
    public Text answerText;
    public GameObject btn_checkAnswer;
    public GameObject winnerPanel;
    public Text p1ScoreText;
    public Text p2ScoreText;
    public Text p3ScoreText;
    public Text p4ScoreText;
    public Text winnerMessage;
    public static int count = 0;
    public static int p1Score = 0;
    public static int p2Score = 0;
    public static int p3Score = 0;
    public static int p4Score = 0;
    public static bool endGame = false;
    public AudioSource source;
    public AudioClip winnerClip;
    public AudioClip deductMarkClip;
    public AudioClip yeahClip;
    public AudioClip correctClip;
    public GameObject ps;
    bool playOnce = false;
    int winningScore;



    void Start()
    {
        endGame = false;
        playOnce = false;
        winningScore = PlayerPrefs.GetInt("winningScore");
/*        path = Application.dataPath + "/StreamingAssets/QuestionDatabase.csv";*/

#if UNITY_EDITOR
        path = Application.dataPath + "/StreamingAssets/QuestionDatabase.csv";
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        path = "jar:file://" + Application.dataPath + "!/assets/QuestionDatabase.csv";
#endif
        winnerPanel.SetActive(false);
        StartCoroutine(LoadQuestionData());
        count = 0;
        p1Score = 0;
        p2Score = 0;
        p3Score = 0;
        p4Score = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            p1Score += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            p2Score += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            p3Score += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            p4Score += 1;
        }
        if (Move.showQuestion == true)
        {
            if(count == 0)
            {
                showQuestion();
                count++;
            }
        }
        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();
        p3ScoreText.text = p3Score.ToString();
        p4ScoreText.text = p4Score.ToString();
        if(p1Score >= winningScore || p2Score >= winningScore || p3Score >= winningScore || p4Score >= winningScore)
        {
            if(playOnce == false)
            {
                if(p1Score >= winningScore && endGame == false)
                {
                    winnerMessage.text = "Congratulation! Red have win this game.";
                }else if(p2Score >= winningScore && endGame == false)
                {
                    winnerMessage.text = "Congratulation! Yellow have win this game.";
                }
                else if(p3Score >= winningScore && endGame == false)
                {
                    winnerMessage.text = "Congratulation! Blue have win this game.";
                }
                else if(p4Score >= winningScore && endGame == false)
                {
                    winnerMessage.text = "Congratulation! Green have win this game.";
                }
                endGame = true;
                ps.SetActive(true);
                winnerPanel.SetActive(true);
                playWinnerSound();
                playOnce = true;
                Move.ResetData();
            }
        }
    }
    public IEnumerator LoadQuestionData()
    {
        questionDatabase.Clear(); //clear database
#if UNITY_EDITOR
        StreamReader strReader = new StreamReader(path); //read CSV files
        Debug.Log(path);
        string line = strReader.ReadLine();
        while (line != null)
        {
            questionDatabase.Add(new Question());
            string[] items = line.Split(',');
            questionDatabase[questionDatabase.Count - 1].SetQuestion(items[0]);
            questionDatabase[questionDatabase.Count - 1].SetAnswer(items[1]);
            line = strReader.ReadLine();
        }   
        strReader.Close();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        //string data = string.Empty;
        //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //StreamReader sr = new StreamReader(fs);
        //data = sr.ReadLine();
        //while (data != null)
        //{
        //    questionDatabase.Add(new Question());
            
        //    string[] items = data.Split(',', '\n');
        //    questionDatabase[questionDatabase.Count - 1].SetQuestion(items[0]);
        //    questionDatabase[questionDatabase.Count - 1].SetAnswer(items[1]);
        //    data = strReader.ReadLine();
        //}   
        //sr.Close();
        //fs.Close();
        WWW www = new WWW(path);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("Can't read");
        }
        string item = www.text;
        string[] list = item.Split(',', '\n');
        for(int i = 0; i < list.Length; i++)
        {
            questionDatabase.Add(new Question());
            questionDatabase[questionDatabase.Count - 1].SetQuestion(list[i]);
            questionDatabase[questionDatabase.Count - 1].SetAnswer(list[i + 1]);
            i++;
        }
#endif
        yield return null;
    }
    public async void showQuestion()
    {
        if (Move.pos == 1)
        {
            int i = Random.Range(0, 3);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 2 || Move.pos == 17 || Move.pos == 33)
        {
            btn_checkAnswer.SetActive(false);
            int i = Random.Range(1, 3);
            playYeahSound();
            questionText.text = "Congratulations! You get " + i + " score";
            Move.showQuestion = false;
            Move.answered = true;
            if (Move.turn == 0)
            {
                p1Score += i;
            }
            else if (Move.turn == 1)
            {
                p2Score += i;
            }
            else if (Move.turn == 2)
            {
                p3Score += i;
            }
            else if (Move.turn == 3)
            {
                p4Score += i;
            }
            await Task.Delay(3000);
        }
        else
        if (Move.pos == 3)
        {
            int i = Random.Range(4, 7);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 8)
        {
            int i = Random.Range(8, 11);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 9)
        {
            int i = Random.Range(12, 15);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 11)
        {
            int i = Random.Range(16, 19);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 13)
        {
            int i = Random.Range(20, 23);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 14)
        {
            int i = Random.Range(24, 27);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 16)
        {
            int i = Random.Range(28, 31);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 18)
        {
            int i = Random.Range(32, 35);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 21)
        {
            int i = Random.Range(36, 39);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 23)
        {
            int i = Random.Range(40, 43);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 26)
        {
            int i = Random.Range(44, 47);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 27)
        {
            int i = Random.Range(48, 51);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 29)
        {
            int i = Random.Range(52, 55);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 31)
        {
            int i = Random.Range(56, 59);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 32)
        {
            int i = Random.Range(60, 63);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 37)
        {
            int i = Random.Range(64, 67);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else
        if (Move.pos == 39)
        {
            int i = Random.Range(68, 71);
            questionText.text = questionDatabase[i].GetQuestion();
            answerText.text = questionDatabase[i].GetAnswer();
        }
        else if (Move.pos == 7 || Move.pos == 22 || Move.pos == 36)
        {
            int loop = 0;
            string symbol;
            int mark;
            btn_checkAnswer.SetActive(false);
            int i = Random.Range(-3, 5);
            if(i < 0)
            {
                symbol = "-";
                mark = -i;
            }else
            {
                symbol = "+";
                mark = i;
            }
            do
            {
                string str_temp = "+";
                int int_temp = 0;
                for(int o = 0; o < 100; o++)
                {
                    if(loop == 0)
                        questionText.text = str_temp + " " + int_temp;
                    if(loop == 1)
                        questionText.text = symbol + " " + int_temp;
                    int_temp++;
                    if(int_temp == 5)
                    {
                        int_temp = 0;
                    }
                    if(str_temp == "+")
                    {
                        str_temp = "-";
                    }
                    else
                    {
                        str_temp = "+";
                    }
                    await Task.Delay(10);
                }
                loop++;
            } while (loop != 2);
            questionText.text = symbol + " " + mark;
            await Task.Delay(1000);
            if (i <= 0)
            {
                playDeductMarkSound();
                questionText.text = "Oh! You get " + i + " score";
            }
            else
            {
                playYeahSound();
                questionText.text = "Congratulations! You get " + i + " score";
            }
            if (Move.turn == 0)
            {
                p1Score = p1Score + i;
            }
            else if (Move.turn == 1)
            {
                p2Score = p2Score + i;
            }
            else if (Move.turn == 2)
            {
                p3Score = p3Score + i;
            }
            else if (Move.turn == 3)
            {
                p4Score = p4Score + i;
            }
            Move.showQuestion = false;
            Move.answered = true;
            await Task.Delay(2000);
        }
        else
        {
            btn_checkAnswer.SetActive(false);
            Move.showQuestion = false;
            Move.answered = true; 
            questionText.text = "+1 Score!!!";
            playCorrectSound();
            if(Move.turn == 0)
            {
                p1Score++;
            }else if(Move.turn == 1)
            {
                p2Score++;
            }else if(Move.turn == 2)
            {
                p3Score++;
            }else if(Move.turn == 3)
            {
                p4Score++;
            }
            await Task.Delay(3000);
        }
    }
    public void playCorrectSound()
    {
        source.PlayOneShot(correctClip);
    }
    public void playWinnerSound()
    {
        source.PlayOneShot(winnerClip);
    }

    public void playDeductMarkSound()
    {
        source.PlayOneShot(deductMarkClip);
    }
    public void playYeahSound()
    {
        source.PlayOneShot(yeahClip);
    }
}
