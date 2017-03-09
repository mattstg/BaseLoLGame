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
    public RectTransform contentParent;
    public RectTransform pagesParent;
    private Image ppImage;
    private ScrollRect ppScrollRect;
    private Mask ppMask;
    public Scrollbar scrollbar;
    public RectTransform buttonsParent;
    public PanelButton nextButton;
    public PanelButton backButton;

    private List<Page> pages = new List<Page>();
    private int currentPage = 0;

    private string defaultNextButtonText = "";
    private string defaultBackButtonText = "";
    private string defaultTitleText = "";
    private float defaultContentTop = 64f;
    private bool hideTitle = false;

    void Start()
    {
        if (pagesParent)
        {
            ppImage = pagesParent.GetComponent<Image>();
            ppScrollRect = pagesParent.GetComponent<ScrollRect>();
            ppMask = pagesParent.GetComponent<Mask>();
        }
        if (nextButton)
            defaultNextButtonText = nextButton.text.text;
        if (backButton)
            defaultBackButtonText = backButton.text.text;
        if (titleText)
            defaultTitleText = titleText.text;
        if (contentParent)
            defaultContentTop = Mathf.Floor((contentParent.sizeDelta.y / 2f + contentParent.anchoredPosition.y) * -1f);

        if (nextButton)
        {
            nextButton.AssignPanelManager(this);
            nextButton.SetMode(false, false);
            nextButton.InitializeState(false, true);
            nextButton.SetActive(true);
        }
        if (backButton)
        {
            backButton.AssignPanelManager(this);
            backButton.SetMode(false, false);
            backButton.InitializeState(false, true);
            backButton.SetActive(false);
        }
        LoadPanel();
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
        bool nextPermission = pages[currentPage].NextRequested();
        if (nextPermission)
        {
            currentPage++;
            PanelFlow();
        }
    }

    public void Back()
    {
        bool backPermission = pages[currentPage].BackRequested();
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
            foreach (Page p in pages)
                p.AssignPanelManager(this);
        }
        currentPage = 0;
        PanelFlow();
    }

    public void ClearPanel()
    {
        pages.Clear();
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
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == currentPage)
            {
                ApplyPageSettings(pages[i]);
                pages[i].gameObject.SetActive(true);
            }
            else
                pages[i].gameObject.SetActive(false);
        }
        if (currentPage == 0)
            backButton.SetActive(false);
        else
            backButton.SetActive(true);
    }

    public void HideTitle(bool becomesHidden)
    {
        if (becomesHidden != hideTitle)
        {
            if (becomesHidden)
            {
                titleText.transform.parent.gameObject.SetActive(false);
                contentParent.sizeDelta = new Vector2(contentParent.sizeDelta.x, contentParent.sizeDelta.y + defaultContentTop);
                contentParent.anchoredPosition = new Vector2(contentParent.anchoredPosition.x, contentParent.anchoredPosition.y + (defaultContentTop / 2));
            }
            else
            {
                titleText.transform.parent.gameObject.SetActive(true);
                contentParent.sizeDelta = new Vector2(contentParent.sizeDelta.x, contentParent.sizeDelta.y - defaultContentTop);
                contentParent.anchoredPosition = new Vector2(contentParent.anchoredPosition.x, contentParent.anchoredPosition.y - (defaultContentTop / 2));
            }
            hideTitle = becomesHidden;
        }
    }

    public void ApplyPageSettings(Page p)
    {
        if (!p)
            return;
        if (p.nextTextOverride)
            nextButton.SetText(p.nextText);
        else
            nextButton.SetText(defaultNextButtonText);
        nextButton.SetEnabled(p.nextEnabled);
        nextButton.SetActive(p.nextActive);

        if (p.backTextOverride)
            backButton.SetText(p.backText);
        else
            backButton.SetText(defaultBackButtonText);
        backButton.SetEnabled(p.backEnabled);
        backButton.SetActive(p.backActive);

        if (p.titleTextOverride)
            titleText.text = p.titleText;
        else
            titleText.text = defaultTitleText;

        HideTitle(p.hideTitle);

        if (ppScrollRect)
            ppScrollRect.content = (RectTransform)p.transform;

        if (p.useScrollbar && !p.useScrollRect)
            p.useScrollRect = true;

        if (p.useScrollbar)
            scrollbar.transform.parent.gameObject.SetActive(true);
        else
            scrollbar.transform.parent.gameObject.SetActive(false);

        if (p.useScrollRect)
            ppScrollRect.enabled = true;
        else
            ppScrollRect.enabled = false;

        if (p.usePageMask)
        {
            ppMask.enabled = true;
            Color c = ppImage.color;
            c.a = 1f;
            ppImage.color = c;
        }
        else
        {
            Color c = ppImage.color;
            c.a = 0f;
            ppImage.color = c;
            ppMask.enabled = false;
        }
    }

    public int GetCurrentPage()
    {
        return currentPage;
    }
}
