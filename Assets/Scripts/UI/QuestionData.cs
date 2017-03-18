using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class QuestionData
{
    public string question = "";
    public string info = "";
    public string correctText = "Correct!";
    public string incorrectText = "Incorrect";
    public List<string> answers = new List<string>();
    private int correctAnswer = -1;

    public QuestionData()
    {

    }

    public void AddAnswer(string answer, bool isCorrect)
    {
        answers.Add(answer);
        if (isCorrect)
            correctAnswer = answers.Count - 1;
    }

    public void AddAnswer(string answer)
    {
        AddAnswer(answer, false);
    }

    public int CorrectAnswer()
    {
        return correctAnswer;
    }

    public void SetCorrectText(string appendix)
    {
        correctText = "Correct! " + appendix;
    }

    public void SetCorrectText()
    {
        SetCorrectText(answers[correctAnswer] + ".");
    }
}
