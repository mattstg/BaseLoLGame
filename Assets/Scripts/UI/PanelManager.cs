using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PanelManager : MonoBehaviour
{
    public PanelID panelID = PanelID.None;
    public Text titleText;
    public Transform pagesParent;
    public Transform buttonsParent;
    public PanelButton nextButton;
    public PanelButton backButton;

    private Page page;
    private List<Page> pages = new List<Page>();
    private int currentPage = 0;

    void Awake()
    {

    }

    void Start()
    {
        LoadPanel();
        if (nextButton)
        {
            nextButton.AssignPanelManager(this);
            nextButton.SetMode(false, false);
            nextButton.InitializeState(false, true);
            nextButton.SetText("Next");
            nextButton.SetActive(true);
        }
        if (backButton)
        {
            backButton.AssignPanelManager(this);
            backButton.SetMode(false, false);
            backButton.InitializeState(false, true);
            backButton.SetText("Back");
            backButton.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void ButtonClicked(PanelButton button)
    {
        if (button == nextButton)
        {
            Next();
        }
        else if (button == backButton)
        {
            Back();
        }
        else
        {

        }
    }

    public void ButtonSelected(PanelButton button)
    {

    }

    public void ButtonDeselected(PanelButton button)
    {

    }

    public void Next()
    {
        bool nextPermission = page.NextRequested();
        if (nextPermission)
        {
            currentPage++;
            PanelFlow();
        }
    }

    public void Back()
    {
        bool backPermission = page.BackRequested();
        if (backPermission && currentPage > 0)
        {
            currentPage--;
            PanelFlow();
        }
    }

    public void LoadPanel()
    {
        if (panelID != PanelID.None && Enum.IsDefined(typeof(PanelID), panelID))
        {
            ClearPanel();
            PageLoader.Instance.LoadPages(panelID, this);
        }
        else        // use preexisting Pages in pagesParent, instead of using PageLoader to generate Pages
        {
            panelID = PanelID.None;
            foreach (Transform t in pagesParent)
                t.gameObject.SetActive(false);
            pagesParent.GetComponentsInChildren<Page>(true, pages);
        }
        currentPage = 0;
        PanelFlow();
    }

    public void ClearPanel()
    {
        foreach (Transform t in pagesParent)
            Destroy(t.gameObject);
        currentPage = 0;
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public virtual void PanelFlow()
    {
        if (pages == null)
        {
            ClearPanel();
            ClosePanel();
        }
        if (currentPage >= pages.Count || currentPage < 0)
        {
            currentPage = 0;
            ClosePanel();
        }
        page = pages[currentPage];
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == currentPage)
                pages[i].gameObject.SetActive(true);
            else
                pages[i].gameObject.SetActive(false);
        }
        if (currentPage == 0)
            backButton.SetActive(false);
        else
            backButton.SetActive(true);
    }

    public int GetCurrentPage()
    {
        return currentPage;
    }
}
