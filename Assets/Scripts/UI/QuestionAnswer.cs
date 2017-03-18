using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class QuestionAnswer : CanvasButton
{
    protected Question question;

    public void AssignQuestion(Question _question)
    {
        question = _question;

    }

    protected override void BecameSelected(bool directAction)
    {
        if (question)
            question.AnswerSelected(this);
    }

    protected override void BecameDeselected(bool directAction)
    {
        if (question && directAction)
            question.AnswerDeselected();
    }
}
