using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class Page : MonoBehaviour
{
    public bool useScrollRect = false;
    public bool useScrollbar = false;
    public bool usePageMask = true;

    public bool titleActive = true;
    public bool titleTextOverride = false;
    public string titleText = "";

    public bool nextActive = true;
    public bool nextEnabled = true;
    public bool nextTextOverride = false;
    public string nextText = "";

    public bool backActive = true;
    public bool backEnabled = true;
    public bool backTextOverride = false;
    public string backText = "";

    public bool infoActive = false;
    public bool infoEnabled = true;
    public bool infoTextOverride = false;
    public string infoText = "";

    protected PanelManager panelManager;
    protected RectTransform pagesParent;
    protected int pageNumber = 0;
    protected int pagesCount = 1;
    [HideInInspector]
    public Text text;
    [HideInInspector]
    public Image image;

    protected bool isOpen = false;

    protected virtual void Awake()
    {
        text = gameObject.GetComponent<Text>();
        image = gameObject.GetComponent<Image>();
    }

    protected virtual void Start()
    {
        if (useScrollRect)
            InitiateScroll();
    }

    public virtual void AssignPanelManager(PanelManager _panelManager)
    {
        panelManager = _panelManager;
        if (!pagesParent && panelManager.pagesParent)
            pagesParent = panelManager.pagesParent;
    }

    public void InitiateScroll()
    {
        if (!pagesParent)
            pagesParent = (RectTransform)transform.parent;
        ScrollRect sr = pagesParent.GetComponent<ScrollRect>();
        if (sr)
            sr.verticalNormalizedPosition = 1;
    }

    public virtual void PageOpened()
    {
        isOpen = true;
    }

    public virtual void PageClosed()
    {
        isOpen = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void SetPageNumber(int _pageNumber, int _pagesCount)
    {
        pageNumber = _pageNumber;
        pagesCount = _pagesCount;
    }

    public virtual bool NextRequested()     // Page children can override button behaviour
    {
        return true;                        // return true/false = request permitted/denied
    }

    public virtual bool BackRequested()
    {
        return true;
    }

    public void ScrollSettings(bool scrRect, bool scrollbar, bool pageMask)
    {
        useScrollRect = scrRect;
        useScrollbar = scrollbar;
        usePageMask = pageMask;
    }

    public void TitleSettings(bool active, bool textOverride, string text)
    {
        titleActive = active;
        titleTextOverride = textOverride;
        titleText = text;
    }

    public void NextSettings(bool active, bool enabled, bool textOverride, string text)
    {
        nextActive = active;
        nextEnabled = enabled;
        nextTextOverride = textOverride;
        nextText = text;
    }

    public void BackSettings(bool active, bool enabled, bool textOverride, string text)
    {
        backActive = active;
        backEnabled = enabled;
        backTextOverride = textOverride;
        backText = text;
    }

    public void InfoSettings(bool active, bool enabled, bool textOverride, string text)
    {
        infoActive = active;
        infoEnabled = enabled;
        infoTextOverride = textOverride;
        infoText = text;
    }

    public virtual void SetTextContent(string str)
    {
        if (text)
            text.text = str;
    }

    public void SetTextAlignment(TextAnchor alignment)
    {
        if (text)
            text.alignment = alignment;
    }

    public void SetTextSize(int fontSize)
    {
        if (text)
        {
            text.fontSize = fontSize;
            text.resizeTextMaxSize = fontSize;
        }
    }

    public void SetImageContent(Sprite sprite)
    {
        if (image)
            image.sprite = sprite;
        AspectRatioMatch arm = gameObject.GetComponent<AspectRatioMatch>();
        if (arm)
            arm.UpdateAspectRatio();
    }

    public void SetImageAlpha(float alpha)
    {
        if (image)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}
