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
    public RectTransform buttonsParent;
    public Scrollbar scrollbar;
    public PanelButton nextButton;
    public PanelButton backButton;
    public PanelButton infoButton;
    public Text infoText;

    private List<Page> pages = new List<Page>();
    private int currentPage = 0;

    private RectTransform infoParent;
    private Image ppImage;
    private ScrollRect ppScrollRect;
    private Mask ppMask;

    private string defaultTitleText = "";
    private string defaultNextButtonText = "";
    private string defaultBackButtonText = "";
    private string defaultInfoText = "";
    private float defaultContentTop = 64f;
    private bool titleActive = true;

    public delegate void ClosePanelCallback();
    private ClosePanelCallback closePanelCallback;

    void Start()
    {
        if (pagesParent)
        {
            ppImage = pagesParent.GetComponent<Image>();
            ppScrollRect = pagesParent.GetComponent<ScrollRect>();
            ppMask = pagesParent.GetComponent<Mask>();
        }
        if (contentParent)
            defaultContentTop = Mathf.Floor((contentParent.sizeDelta.y / 2f + contentParent.anchoredPosition.y) * -1f);
        if (titleText)
            defaultTitleText = titleText.text;
        if (nextButton)
        {
            nextButton.AssignPanelManager(this);
            nextButton.SetMode(false, false);
            nextButton.InitializeState(false, true);
            defaultNextButtonText = nextButton.text.text;
            nextButton.SetActive(true);
        }
        if (backButton)
        {
            backButton.AssignPanelManager(this);
            backButton.SetMode(false, false);
            backButton.InitializeState(false, true);
            defaultBackButtonText = backButton.text.text;
            backButton.SetActive(false);
        }
        if (infoButton)
        {
            infoButton.AssignPanelManager(this);
            infoButton.SetMode(true, true);
            infoButton.InitializeState(false, true);
            infoParent = (RectTransform)infoButton.transform.parent;
            infoButton.SetActive(true);
        }
        if (infoText)
        {
            defaultInfoText = infoText.text;
            infoText.gameObject.SetActive(false);
        }
        if (infoParent)
            infoParent.gameObject.SetActive(false);
        LoadPanel(Empty);
    }

    public void Empty() { } // just for development, while LoadPanel(CPC) is
                            // called here from Start() instead of from GameFlow

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
        if (button == infoButton)
            InfoOn();
    }

    public void ButtonDeselected(PanelButton button)
    {
        if (button == infoButton)
            InfoOff();
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

    public void InfoOn()
    {
        if (infoText)
            infoText.gameObject.SetActive(true);
    }

    public void InfoOff()
    {
        if (infoText)
            infoText.gameObject.SetActive(false);
    }

    public void LoadPanel(ClosePanelCallback callback)
    {
        closePanelCallback = callback;
        if (panelID != PanelID.None && Enum.IsDefined(typeof(PanelID), panelID))
        {                                           // use PageLoader to generate pages
            ClearPanel();
            pages = PageLoader.Instance.LoadPages(panelID, this);
        }
        else
        {                                           // use preexisting Pages in pagesParent
            panelID = PanelID.None;
            pagesParent.GetComponentsInChildren<Page>(true, pages);
            int pagesCount = pages.Count;
            for (int i = 0; i < pagesCount; i++)
            {
                pages[i].AssignPanelManager(this);
                pages[i].SetPageNumber(i, pagesCount);
            }
        }
        currentPage = 0;
        OpenPanel();
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
        closePanelCallback();
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
                pages[i].PageOpened();
            }
            else
            {
                if (pages[i].IsOpen())
                    pages[i].PageClosed();
                pages[i].gameObject.SetActive(false);
            }
        }
        if (currentPage == 0)
            backButton.SetActive(false);
        else
            backButton.SetActive(true);
    }

    public void SetTitleActive(bool becomesActive)
    {
        if (becomesActive != titleActive)
        {
            if (becomesActive)
            {
                titleText.transform.parent.gameObject.SetActive(true);
                contentParent.sizeDelta = new Vector2(contentParent.sizeDelta.x, contentParent.sizeDelta.y - defaultContentTop);
                contentParent.anchoredPosition = new Vector2(contentParent.anchoredPosition.x, contentParent.anchoredPosition.y - (defaultContentTop / 2f));
            }
            else
            {
                titleText.transform.parent.gameObject.SetActive(false);
                contentParent.sizeDelta = new Vector2(contentParent.sizeDelta.x, contentParent.sizeDelta.y + defaultContentTop);
                contentParent.anchoredPosition = new Vector2(contentParent.anchoredPosition.x, contentParent.anchoredPosition.y + (defaultContentTop / 2f));
            }
            titleActive = becomesActive;
        }
    }

    public void ApplyPageSettings(Page p)
    {
        if (!p)
            return;

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

        SetTitleActive(p.titleActive);
        if (p.titleTextOverride)
            titleText.text = p.titleText;
        else
            titleText.text = defaultTitleText;

        nextButton.SetActive(p.nextActive);
        nextButton.SetEnabled(p.nextEnabled);
        if (p.nextTextOverride)
            nextButton.SetText(p.nextText);
        else
            nextButton.SetText(defaultNextButtonText);

        backButton.SetActive(p.backActive);
        backButton.SetEnabled(p.backEnabled);
        if (p.backTextOverride)
            backButton.SetText(p.backText);
        else
            backButton.SetText(defaultBackButtonText);

        infoParent.gameObject.SetActive(p.infoActive);
        infoButton.SetEnabled(p.infoEnabled);
        infoButton.SetSelected(false, false);
        if (p.infoTextOverride)
            infoText.text = p.infoText;
        else
            infoText.text = defaultInfoText;
        infoText.gameObject.SetActive(false);
    }

    public int GetCurrentPage()
    {
        return currentPage;
    }
}
