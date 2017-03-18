using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class QuestionInfo : CanvasButton
{
    public Sprite correctSprite;
    public Sprite incorrectSprite;
    protected Question question;

    public void AssignQuestion(Question _question)
    {
        question = _question;
    }

    protected override void BecameSelected(bool directAction)
    {
        question.QuestionFlow(false);
    }

    protected override void BecameDeselected(bool directAction)
    {
        question.QuestionFlow(false);
    }

    public void SetDefault()
    {
        image.sprite = defaultSprite;
        if (isSelected)
        {
            isSelected = false;
            BecameDeselected(false);
        }
        SetEnabled(true);
    }

    public void SetCorrect()
    {
        image.sprite = correctSprite;
        if (isSelected)
        {
            isSelected = false;
            BecameDeselected(false);
        }
        SetEnabled(false);
    }

    public void SetIncorrect()
    {
        image.sprite = incorrectSprite;
        if (isSelected)
        {
            isSelected = false;
            BecameDeselected(false);
        }
        SetEnabled(false);
    }
}
