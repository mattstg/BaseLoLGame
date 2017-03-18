using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum QuizStatus {Answering, SomeIncorrect, AllCorrect};

public class QuizPage : Page
{
    public QuizID quizID = QuizID.None;
    [HideInInspector]
    public RectTransform questionsParent;
    protected List<Question> questions = new List<Question>();
    protected QuizStatus status = QuizStatus.Answering;

    protected int submissionAttempts = 0;

    protected override void Awake()
    {
        questionsParent = (RectTransform)transform;
        if (quizID != QuizID.None)
            LoadQuiz();
    }

    public void LoadQuiz(QuizID id)
    {
        quizID = id;
        LoadQuiz();
    }

    public void LoadQuiz()
    {
        ClearQuestions();
        LoadQuestions();
    }

    public void ClearQuestions()
    {
        foreach (Transform t in questionsParent)
        {
            Destroy(t.gameObject);
        }
    }

    public void LoadQuestions()
    {
        questions = QuestionLoader.Instance.LoadQuestions(quizID, this);
    }

    public override void PageOpened()
    {
        base.PageOpened();
        UpdateState(false);
    }

    public override bool NextRequested()
    {
        return QuizFlow(true);
    }

    public void QuestionAnswered()
    {
        UpdateState(false);
    }

    public bool QuizFlow(bool next)
    {
        if (next)
        {
            switch (status)
            {
                case QuizStatus.Answering:
                    if (AllCorrect())
                        status = QuizStatus.AllCorrect;
                    else
                        status = QuizStatus.SomeIncorrect;
                    break;
                case QuizStatus.SomeIncorrect:
                    status = QuizStatus.Answering;
                    break;
                case QuizStatus.AllCorrect:
                    return true;            // control of NextButton is returned to PanelManager
                default:
                    break;
            }
        }
        UpdateState(next);
        return false;
    }

    public void UpdateState(bool statusChange)
    {
        switch (status)
        {
            case QuizStatus.Answering:
                foreach (Question q in questions)
                    q.QuestionFlow(statusChange);
                panelManager.nextButton.SetText("Submit");
                if (AllAnswered())
                    panelManager.nextButton.SetEnabled(true);
                else
                    panelManager.nextButton.SetEnabled(false);
                break;
            case QuizStatus.SomeIncorrect:
                panelManager.nextButton.SetText("Try again");
                panelManager.nextButton.SetEnabled(true);
                foreach (Question q in questions)
                    q.QuestionFlow(statusChange);
                break;
            case QuizStatus.AllCorrect:
                panelManager.nextButton.SetText("Perfect!");
                panelManager.nextButton.SetEnabled(true);
                foreach (Question q in questions)
                    q.QuestionFlow(statusChange);
                break;
            default:
                break;
        }
        
        if (statusChange)
        {
            switch (status)
            {
                case QuizStatus.Answering:

                    break;
                case QuizStatus.SomeIncorrect:

                    break;
                case QuizStatus.AllCorrect:

                    break;
                default:
                    break;
            }
        }
    }

    public int AnsweredCount()
    {
        int count = 0;
        foreach (Question q in questions)
        {
            if (q.IsAnswered())
                count++;
        }
        return count;
    }

    public int CorrectCount()
    {
        int count = 0;
        foreach (Question q in questions)
        {
            if (q.IsCorrect())
                count++;
        }
        return count;
    }

    public int TotalCount()
    {
        return questions.Count;
    }

    public bool AllAnswered()
    {
        return AnsweredCount() == TotalCount();
    }

    public bool AllCorrect()
    {
        return CorrectCount() == TotalCount();
    }
}
