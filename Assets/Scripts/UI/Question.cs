using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum QuestionStatus {Answering, Info, Evaluating, EvaluatedIncorrect, EvaluatedCorrect};

public class Question : MonoBehaviour
{
    public Text questionText;
    public QuestionInfo infoButton;
    public Text infoText;
    public RectTransform answersParent;
    protected List<QuestionAnswer> answers = new List<QuestionAnswer>();
    public QuestionData data;

    protected QuizPage quiz;
    protected QuestionStatus status = QuestionStatus.Answering;

    void Awake()
    {
        if (!quiz)                                      //
            quiz = GetComponentInParent<QuizPage>();    // just for development
        answersParent.GetComponentsInChildren<QuestionAnswer>(true, answers);
        foreach (QuestionAnswer a in answers)
        {
            a.AssignQuestion(this);
        }
        infoButton.AssignQuestion(this);
    }

    public void AssignQuiz(QuizPage _quiz)
    {
        quiz = _quiz;
    }

    public void ApplyData()
    {
        questionText.text = data.question;
        infoText.text = data.info;
        for (int i = 0; i < answers.Count; i++)
        {
            if (i < data.answers.Count)
            {
                answers[i].SetText(data.answers[i]);
                answers[i].SetActive(true);
            }
            else
            {
                answers[i].SetText("");
                answers[i].SetActive(false);
            }
        }
        if (data.CorrectAnswer() < 0 || data.CorrectAnswer() >= data.answers.Count)
            Debug.LogError("No correct answer assigned for question: " + data.question);
    }

    public void AnswerSelected(QuestionAnswer answer)
    {
        foreach (QuestionAnswer a in answers)
        {
            if (a != answer)
                a.SetSelected(false, false);
        }
        if (quiz)
            quiz.QuestionAnswered();
    }

    public void AnswerDeselected()
    {
        if (quiz)
            quiz.QuestionAnswered();
    }

    public void QuestionFlow(bool next)
    {
        if (next)
        {
            switch (status)
            {
                case QuestionStatus.Answering:
                    status = QuestionStatus.Evaluating;
                    break;
                case QuestionStatus.Info:
                    status = QuestionStatus.Evaluating;
                    break;
                case QuestionStatus.EvaluatedIncorrect:
                    status = QuestionStatus.Answering;
                    break;
                case QuestionStatus.EvaluatedCorrect:
                    next = false;
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (status == QuestionStatus.Answering && infoButton.IsSelected())
            {
                status = QuestionStatus.Info;
            }
            else if (status == QuestionStatus.Info && !infoButton.IsSelected())
            {
                status = QuestionStatus.Answering;
            }
        }

        if (status == QuestionStatus.Evaluating)
        {
            if (IsCorrect())
                status = QuestionStatus.EvaluatedCorrect;
            else
                status = QuestionStatus.EvaluatedIncorrect;
        }

        UpdateState(next);
    }

    public void UpdateState(bool thoroughUpdate)
    {
        switch (status)
        {
            case QuestionStatus.Answering:
                infoText.gameObject.SetActive(false);
                answersParent.gameObject.SetActive(true);
                break;
            case QuestionStatus.Info:
                infoText.gameObject.SetActive(true);
                answersParent.gameObject.SetActive(false);
                break;
            default:
                break;
        }

        if (thoroughUpdate)                     // thoroughUpdate should be true only when moving
        {                                       // between Answering/Info and EvaluatedIncorrect/Correct
            switch (status)
            {
                case QuestionStatus.Answering:
                    infoButton.SetDefault();
                    infoText.text = data.info;
                    infoText.gameObject.SetActive(false);
                    answersParent.gameObject.SetActive(true);
                    foreach (QuestionAnswer a in answers)
                        a.SetSelected(false, false);
                    break;
                case QuestionStatus.Info:       // for safety; this should never occur
                    infoButton.SetDefault();
                    infoButton.SetSelected(true, false);
                    infoText.text = data.info;
                    infoText.gameObject.SetActive(false);
                    answersParent.gameObject.SetActive(true);
                    foreach (QuestionAnswer a in answers)
                        a.SetSelected(false, false);
                    break;
                case QuestionStatus.EvaluatedIncorrect:
                    infoButton.SetIncorrect();
                    infoText.text = data.incorrectText;
                    infoText.gameObject.SetActive(true);
                    answersParent.gameObject.SetActive(false);
                    break;
                case QuestionStatus.EvaluatedCorrect:
                    infoButton.SetCorrect();
                    infoText.text = data.correctText;
                    infoText.gameObject.SetActive(true);
                    answersParent.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }

    public bool IsAnswered()
    {
        bool answered = false;
        foreach (QuestionAnswer a in answers)
        {
            if (a.IsSelected())
                answered = true;
        }
        return answered;
    }

    public bool IsCorrect()
    {
        return answers[data.CorrectAnswer()].IsSelected();
    }
}
