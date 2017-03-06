using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class ScoreScreenManager : MonoBehaviour {

    public static string nextSceneName;
    public static LessonType thisLesson;

    public Transform textParent;

    public void Start()
    {
        SetScore();
    }

    public void SetScore()
    {
        int totalScore = 0;
        foreach (Transform t in textParent)
        {
            float _value = 0;
            bool truncate = false; //if false, its int, else float
            bool isPercentage = false;
            //if (t.name == "Total")
            //{
            //    //_value = ProgressTracker.Instance.GetTotalScore(); do at bottom, float point errors show
            //}
            if (t.name != "Total")
            {
                string[] splitName = t.name.Split('-');
                LessonType lessonType = (LessonType)System.Enum.Parse(typeof(LessonType), splitName[0], true);
                if (lessonType > thisLesson)
                {
                    t.gameObject.SetActive(false);
                }
                switch (splitName[1])
                {
                    case "GS":
                        _value = ProgressTracker.Instance.GetRoundScore(lessonType);
                        break;
                    case "QM":
                        //truncate = true;
                        //_value = ProgressTracker.Instance.GetMultScore(lessonType);
                        _value = ProgressTracker.Instance.GetMultPercentage(lessonType);
                        isPercentage = true;
                        break;
                    case "T":
                        _value = ProgressTracker.Instance.GetMultScore(lessonType) * ProgressTracker.Instance.GetRoundScore(lessonType);
                        totalScore += (int)_value;
                        break;
                    default:
                        Debug.Log("you done goofed");
                        _value = 0;
                        break;
                }
            }
            string str;
            if (truncate)
            {
                float helper = _value * 100;
                int cutter = (int)helper;
                float backTo = cutter / 100f;
                str = string.Format("{0:0.##}", backTo);
            }
            else
                str = ((int)_value).ToString();
            if (isPercentage)
                str += "%";
            t.GetComponent<Text>().text = str;
        }
        textParent.transform.FindChild("Total").GetComponent<Text>().text = totalScore.ToString();
    }

    public void NextButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
