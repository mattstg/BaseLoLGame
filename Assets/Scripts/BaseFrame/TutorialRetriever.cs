using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class TutorialRetriever  {

    #region Singleton
    private static TutorialRetriever instance;

    public static TutorialRetriever Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TutorialRetriever();
            }
            return instance;
        }
    }
    #endregion
    

    private TutorialRetriever()
    {
    }

    public Sprite GetTutorialImage(LessonType lessonType, int _index)
    {
        return Resources.Load<Sprite>("Tutorial/" + lessonType + _index);
    }
}
