using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum PanelID
{
    None, LevelA, LevelB, LevelC
};

public class PageLoader : MonoBehaviour
{

    #region Singleton
    private static PageLoader instance;

    private PageLoader() { }

    public static PageLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PageLoader();
            }
            return instance;
        }
    }
    #endregion

    public void LoadPages(PanelID panelID, PanelManager panelManager)
    {
        Transform pagesParent = panelManager.pagesParent;
        Text titleText = panelManager.titleText;
        switch (panelID)
        {
            case PanelID.LevelA:

                break;
            case PanelID.LevelB:

                break;
            case PanelID.LevelC:

                break;
            default:

                break;
        }
    }
}
