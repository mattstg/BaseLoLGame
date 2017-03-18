using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum QuizID
{
    LevelA, LevelB, LevelC, LevelD, QuizTutorial, None
};

public class QuestionLoader
{
    private string questionsPrefabDirectory = "Prefabs/UI/Elements/Questions/";
    private string questionSingle = "QuestionSingle";
    private string questionDouble = "QuestionDouble";

    #region Singleton
    private static QuestionLoader instance;

    private QuestionLoader() { }

    public static QuestionLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestionLoader();
            }
            return instance;
        }
    }
    #endregion

    public List<Question> LoadQuestions(QuizID quizID, QuizPage quizPage)
    {
        RectTransform questionsParent = quizPage.questionsParent;
        List<Question> questions = new List<Question>();

        switch (quizID)
        {
            case QuizID.LevelA:

                break;
            case QuizID.LevelB:

                break;
            case QuizID.LevelC:

                break;
            case QuizID.LevelD:

                break;
            case QuizID.QuizTutorial:

                /*  
                    For each question:
                    1.  Create a new QuestionData instance
                    2.  Set strings qd.question and .info
                    3.  Add answers sequentially by using qd.AddAnswer():
                            void AddAnswer(string answer, bool isCorrect), or
                            void AddAnswer(string answer)
                                For simplicity, you can use the second one for incorrect answers,
                                but be sure to use the first one for the correct answer
                    4.  After this, you can use qd.SetCorrectText() to append a string to
                        string qd.correctText, which starts out as "Correct!":
                            void SetCorrectText(string appendix), or
                            void SetCorrectText()
                                Use the second one to automatically append the text of the
                                correct answer to the end of "Correct!"
                    5.  After setting the QuestionData as needed, use this.AddQuestion():
                            void AddQuestion(string prefabName, QuestionData data, QuizPage quizPage, RectTransform questionsParent, List<Question> questions)
                                prefabName: either this.questionSingle or .questionDouble
                                    questionSingle prefab has lower profile, more balanced layout,
                                        with questions and answers aligned on a single line,
                                        but might have too little space to properly display
                                        lengthier questions or answers
                                    questionDouble prefab displays the question on one line,
                                        and the infoButton, infoText, and answers on a second line
                                    avoid mixing these two prefabs within the same quiz
                                data: the QuestionData just generated
                                For the final three variables, always use:
                                    "quizPage, questionsParent, questions"
                    6.  For efficiency, set questions.Capacity to the total number of Questions to be added,
                        before the initial AddQuestion() call.
                */

                questions.Capacity = 3;     // set List<Question> capacity in advance for efficiency

                QuestionData a = new QuestionData();
                a.question = "Question text?";
                a.info = "Answer 2 is correct.";
                a.AddAnswer("Answer 1");
                a.AddAnswer("Answer 2", true);  // correct answer
                a.AddAnswer("Answer 3");
                a.SetCorrectText();         // correctText = "Correct! Answer 2."
                AddQuestion(questionSingle, a, quizPage, questionsParent, questions);

                QuestionData b = new QuestionData();
                b.question = "How many uranium atoms are there in a molecule of water?";
                b.info = "Hint: not one million.";
                b.AddAnswer("One");
                b.AddAnswer("Two");
                b.AddAnswer("Seventeen", true);
                b.AddAnswer("One million");
                b.SetCorrectText("Exactly seventeen.");
                    // correctText = "Correct! Exactly seventeen." 
                AddQuestion(questionSingle, b, quizPage, questionsParent, questions);

                QuestionData c = new QuestionData();
                c.question = "If you need extra space for questions or answers, " +
                    "you can use the questionDouble prefab instead.\n" +
                    "Avoid mixing questionSingles and questionDoubles in the same quiz. Like this. Don't do this.";
                c.info = "Hint: choose the correct answer.";
                c.AddAnswer("Correct", true);
                c.AddAnswer("Incorrect");           // these two lines
                c.AddAnswer("Incorrect", false);    // are equivalent
                AddQuestion(questionDouble, c, quizPage, questionsParent, questions);

                break;
            default:
                break;
        }
        return questions;
    }

    public void AddQuestion(string prefabName, QuestionData data, QuizPage quizPage, RectTransform questionsParent, List<Question> questions)
    {
        prefabName = questionsPrefabDirectory + prefabName;
        GameObject go = Object.Instantiate(Resources.Load(prefabName)) as GameObject;
        go.transform.SetParent(questionsParent, false);
        Question q = go.GetComponent<Question>();
        q.data = data;
        q.ApplyData();
        q.AssignQuiz(quizPage);
        questions.Add(q);
    }
}
