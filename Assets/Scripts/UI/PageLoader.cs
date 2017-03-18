using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum PanelID
{
    LevelA, LevelB, LevelC, LevelD, PanelTutorial, None
};

public class PageLoader
{
    private string pagePrefabDirectory = "Prefabs/UI/Elements/Pages/";

    private string simpleText = "SimpleTextPage";
    private string scrollText = "ScrollTextPage";
    private string cascadeText = "CascadeTextPage";
    private string simpleImage = "SimpleImagePage";
    private string scrollImage = "ScrollImagePage";
    private string quiz = "QuizPage";

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

                break;
            case PanelID.LevelB:

                break;
            case PanelID.LevelC:

                break;
            case PanelID.LevelD:

                break;


            case PanelID.PanelTutorial:

                /*  
                    For each page:
                    1.  Declare an instance of Page (base/derived class)* as the return of this.AddPage():
                            Page AddPage(string prefabName, PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
                                prefabName: choose from the strings declared above:
                                    simpleText / scrollText / cascadeText
                                    simpleImage / scrollImage
                                    quiz
                                For the other three variables, always use:
                                    "panelManager, pagesParent, pages"
                      * Note on Page base/derived classes:
                            Most of the prefabs use the simple base Page class,
                            but some of them use specialized derived classes.
                            Be sure to declare the right type of class based on
                            what kind of page you are creating, and cast AddPage() to that type
                                Page:               simpleText / scrollText
                                                    simpleImage / scrollImage
                                CascadeTextPage:    cascadeText
                                QuizPage:           quiz
                    2.  Set the relevant Panel settings for the Page.
                        These five Page methods apply to the appearance/function of the Panel container,
                        rather than the Page content, and are applied by PanelManager.ApplyPageSettings()
                        every time a page is (re)opened.
                        Refer to the Page class to see the default values.
                            void ScrollSettings(bool scrRect, bool scrollbar, bool pageMask)
                            void TitleSettings(bool active, bool textOverride, string text)
                            void NextSettings(bool active, bool enabled, bool textOverride, string text)
                            void BackSettings(bool active, bool enabled, bool textOverride, string text)
                            void InfoSettings(bool active, bool enabled, bool textOverride, string text)
                                If you need to change these properties at some point after the Panel has
                                started displaying the Page, you can either modify them directly
                                through PanelManager, or else update the settings from within the Page,
                                and then call panelManager.ApplyPageSettings(panelManager.GetCurrentPage())
                    3.  Set the relevant Page settings.
                        These directly affect Page content.
                            Page:
                               *virtual void SetTextContent(string str)
                                void SetTextAlignment(TextAnchor alignment)
                                void SetTextSize(int fontSize)
                                void SetImageContent(Sprite sprite)
                                void SetImageAlpha(float alpha)
                            CascadeTextPage:
                               *override void SetTextContent(string str)
                                void CascadeTextSettings(float charactersPerSecond, bool autoScroll, float smoothTime, IfExceedsPage ifExceedsPage)
                                    For autoscroll, these values are good:
                                    charactersPerSecond = 60f  /  smoothTime = 3f
                            QuizPage:
                                void LoadQuiz(QuizID id)
                    4.  Make any other custom adjustments as needed by directly modifying values as needed.
                    5.  For efficiency, set pages.Capacity to the total number of Pages to be added,
                        before the initial AddPage() call.
                */

                pages.Capacity = 6;

                Page a = AddPage(simpleText, panelManager, pagesParent, pages);
                a.SetTextContent("SimpleTextPage prefab, generated by PageLoader.\n\n" +
                    "The panel is in its default state (except for this text content).\n\n" +
                    "(Back button is automatically inactive on first page.)");

                Page b = AddPage(simpleText, panelManager, pagesParent, pages);
                b.TitleSettings(false, true, "This title is invisible");
                    // when title is set inactive, page content is automatically stretched upward
                b.NextSettings(true, true, true, ">");
                b.BackSettings(true, false, true, "<");
                b.InfoSettings(true, true, true, "Information text.");
                b.SetTextAlignment(TextAnchor.UpperRight);
                b.SetTextSize(22);
                b.SetTextContent("Title has been set inactive. Page content has been automatically stretched upward.\n\n" +
                    "Back button has been disabled.\n\n" +
                    "Info has been activated, with toggle button in lower left corner.\n\n" +
                    "Text alignment and font size have been modified.");

                QuizPage c = (QuizPage)AddPage(quiz, panelManager, pagesParent, pages);
                c.TitleSettings(true, true, "QuizPage: Quiz Tutorial");
                c.LoadQuiz(QuizID.QuizTutorial);
                    // quiz content is defined in QuestionLoader
                    // no need to set NextButton text; QuizPage manages this automatically

                Page d = AddPage(simpleImage, panelManager, pagesParent, pages);
                d.TitleSettings(true, true, "SimpleImagePage");
                d.NextSettings(true, true, true, "Onward!");
                d.SetImageContent(Resources.Load<Sprite>("Graphics/UI/button.selected"));
                d.SetImageAlpha(0.6f);

                Page e = AddPage(scrollImage, panelManager, pagesParent, pages);
                e.TitleSettings(true, true, "ScrollImagePage");
                e.SetImageContent(Resources.Load<Sprite>("Graphics/fish.circuit"));

                CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
                f.SetTextContent("Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y " +
                    "zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh " +
                    "ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs " +
                    "t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc " +
                    "defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq " +
                    "rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. " +
                    "Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij " +
                    "klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv " +
                    "wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh " +
                    "ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u " +
                    "vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, " +
                    "hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw " +
                    "xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i " +
                    "jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx " +
                    "y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij ");
                f.TitleSettings(true, true, "CascadeTextPage");
                f.CascadeTextSettings(150f, true, 3f, IfExceedsPage.ExtendDownward);
                f.NextSettings(true, true, true, "Goodbye");

                break;
            default:
                break;
        }
        int pagesCount = pages.Count;
        for (int i = 0; i < pagesCount; i++)
            pages[i].SetPageNumber(i, pagesCount);
        return pages;
    }

    public Page AddPage(string prefabName, PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
        prefabName = pagePrefabDirectory + prefabName;
        GameObject go = Object.Instantiate(Resources.Load(prefabName)) as GameObject;
        go.transform.SetParent(pagesParent, false);
        Page page = go.GetComponent<Page>();
        page.AssignPanelManager(panelManager);
        pages.Add(page);
        return page;
    }
}
