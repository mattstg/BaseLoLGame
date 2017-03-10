﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum PanelID
{
    None, LevelA, LevelB, LevelC, PanelTutorial
};

public class PageLoader : MonoBehaviour
{
    private string pagePrefabDirectory = "Prefabs/UI/Elements/Pages/";
    private string graphicsDirectory = "Graphics/";

    private string simpleText = "SimpleTextPage";
    private string scrollText = "ScrollTextPage";
    //private string cascadeText = "CascadeTextPage";
    private string simpleImage = "SimpleImagePage";
    private string scrollImage = "ScrollImagePage";
    //private string quiz = "QuizPage";

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

    public List<Page> LoadPages(PanelID panelID, PanelManager panelManager)
    {
        RectTransform pagesParent = panelManager.pagesParent;
        List<Page> pages = new List<Page>();
        if (!panelManager || !pagesParent)
            return pages;

        switch (panelID)
        {
            case PanelID.LevelA:
                pages.Capacity = 5;     // set List<Page> capacity in advance for efficiency

                Page a = AddPagePrefab(simpleText, panelManager, pagesParent, pages);
                a.SetTextContent("SimpleTextPage prefab, generated by PageLoader.\n\n" +
                    "The panel is in its default state (except for this text content) and title/button/info text from the editor is left unchanged.\n\n" +
                    "(Back button is automatically inactive on first page.)");

                Page b = AddPagePrefab(simpleText, panelManager, pagesParent, pages);
                b.TitleSettings(false, true, "Invisible title");
                b.NextSettings(true, true, true, "wow");
                b.BackSettings(true, false, true, "heck");
                b.InfoSettings(true, true, true, "much information very knowledge wow doing me a lern");
                b.SetTextContent("much text such writing very alignment wow\nbig text no title doin me an amaze\ncan go back? nope\nheckin bamboozlin");
                b.SetTextAlignment(TextAnchor.MiddleRight);
                b.SetTextSize(36);

                Page c = AddPagePrefab(simpleText, panelManager, pagesParent, pages);
                c.TitleSettings(true, true, "A different title");
                c.NextSettings(true, true, true, ">");
                c.BackSettings(true, true, true, "<");
                c.SetTextContent("More text, yes!");
                c.SetTextAlignment(TextAnchor.MiddleLeft);

                Page d = AddPagePrefab(simpleImage, panelManager, pagesParent, pages);
                d.TitleSettings(true, true, "SimpleImagePage");
                d.NextSettings(true, true, true, "Onward!");
                d.SetImageContent(Resources.Load<Sprite>(graphicsDirectory + "UI/button.selected"));
                Color dColor = new Color(1f, 0.5f, 0.2f, 0.6f);
                d.image.color = dColor;

                Page e = AddPagePrefab(scrollImage, panelManager, pagesParent, pages);
                e.TitleSettings(true, true, "ScrollImagePage");
                e.SetImageContent(Resources.Load<Sprite>(graphicsDirectory + "fish.circuit"));

                break;
            case PanelID.LevelB:

                break;
            case PanelID.LevelC:

                break;
            default:

                break;
        }
        int pagesCount = pages.Count;
        for (int i = 0; i < pagesCount; i++)
            pages[i].SetPageNumber(i, pagesCount);
        return pages;
    }

    public Page AddPagePrefab(string prefabName, PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
        prefabName = pagePrefabDirectory + prefabName;
        GameObject go = Instantiate(Resources.Load(prefabName)) as GameObject;
        go.transform.SetParent(pagesParent, false);
        Page page = go.GetComponent<Page>();
        page.AssignPanelManager(panelManager);
        pages.Add(page);
        return page;
    }
}
