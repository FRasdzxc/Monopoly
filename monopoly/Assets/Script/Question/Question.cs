using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string question;
    public string answer;

    public void SetQuestion(string q)
    {
        question = q;
    }
    public void SetAnswer(string a)
    {
        answer = a;
    }
    public string GetQuestion()
    {
        return question;
    }
    public string GetAnswer()
    {
        return answer;
    }
}
