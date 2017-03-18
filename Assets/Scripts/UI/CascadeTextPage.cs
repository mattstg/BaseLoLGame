using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum CascadeTextStatus{Ready, Writing, Finished};
public enum IfExceedsPage{BestFitFontSize, ExtendDownward};

public class CascadeTextPage : Page
{
    public RectTransform textTransform;
    protected RectTransform pageTransform;
    protected RectTransform buttonsParent;

    [TextArea(3,8)]
    public string finalText = "";
    private string activeText = "";
    private string tagsAppendix = "";

    public float charactersPerSecond = 60f;
    private int currentCharacter = 0;
    private float timeElapsed = 0f;
    protected CascadeTextStatus status = CascadeTextStatus.Ready;

    public IfExceedsPage ifExceedsPage = IfExceedsPage.BestFitFontSize;
    public bool autoScroll = true;
    public float smoothTime = 3f;
    public float offset = 0f;
    private float velocity = 0f;
    private float targetPos = 0f;

    private float pDeltaVelocity = 0f;
    private Vector2 previousParentDimensions;
    private int originalFontSize = 26;
    private bool fontResized = false;


    protected override void Awake()
    {
        image = gameObject.GetComponent<Image>();
        if (textTransform)
            text = textTransform.GetComponent<Text>();
        else
            text = GetComponentInChildren<Text>();
        originalFontSize = text.fontSize;
        pageTransform = (RectTransform)transform;
    }

    public override void AssignPanelManager(PanelManager _panelManager)
    {
        base.AssignPanelManager(_panelManager);
        buttonsParent = panelManager.buttonsParent;
    }

    void Update()
    {
        if (!isOpen || status == CascadeTextStatus.Ready)
            return;

        if (status == CascadeTextStatus.Writing)
        {
            timeElapsed += Time.deltaTime;
            while (timeElapsed >= (1f / charactersPerSecond) && currentCharacter < finalText.Length)
            {
                HandleTags();
                timeElapsed -= (1f / charactersPerSecond);
                activeText += finalText[currentCharacter++];
                text.text = activeText + tagsAppendix;
            }
            if (currentCharacter >= finalText.Length)
                FinishWriting();
        }
        else // if (status == CascadeTextStatus.Finished)
        {

        }

        UpdateTextPosition();
    }

    public void HandleTags()
    {
        while (finalText[currentCharacter] == '<')          //entering a tag
        {
            activeText += finalText[currentCharacter++];
            if (finalText[currentCharacter] == '/')         //end tag
            {
                while (finalText[currentCharacter] != '>')
                    activeText += finalText[currentCharacter++];
                activeText += finalText[currentCharacter++];
                int appendixLength = tagsAppendix.Length;
                if (appendixLength > 0)
                {
                    int i = 0;
                    while (tagsAppendix[i] != '>')
                    {
                        i++;
                    }
                    i++;
                    appendixLength -= i;
                    tagsAppendix = new string(tagsAppendix.ToCharArray(i, appendixLength));
                }
            }
            else                                            //start tag
            {
                string tagName = "";
                while (finalText[currentCharacter] != '=' &&
                    finalText[currentCharacter] != '>')
                {
                    activeText += finalText[currentCharacter];
                    tagName += finalText[currentCharacter];
                    currentCharacter++;
                }
                tagsAppendix = "<" + "/" + tagName + ">" + tagsAppendix;
                while (finalText[currentCharacter] != '>')
                    activeText += finalText[currentCharacter++];
                activeText += finalText[currentCharacter++];
            }
        }
    }

    public void UpdateTextPosition()
    {
        Vector2 parentDimensions = new Vector2(pagesParent.rect.width, pagesParent.rect.height);
        float pageHeight = 0f;
        float textHeight = textTransform.sizeDelta.y;
        float pDeltaTarget = 0f;
        bool resized = (parentDimensions != previousParentDimensions);
        if (fontResized)
        {
            resized = true;
            fontResized = false;
        }

        if (ifExceedsPage == IfExceedsPage.BestFitFontSize)
        {
            pageTransform.sizeDelta = new Vector2(0, 0);
            pageHeight = pageTransform.rect.height;
            if (textHeight > pageHeight && text.fontSize > 8)
            {
                text.fontSize--;
                //resized = true;
                fontResized = true;
            }
            if (textHeight < pageHeight - 100 && text.fontSize < originalFontSize)
            {
                text.fontSize++;
                //resized = true;
                fontResized = true;
            }
        }
        else if (ifExceedsPage == IfExceedsPage.ExtendDownward)
        {
            if (textHeight > parentDimensions.y)
            {
                pDeltaTarget = textHeight - parentDimensions.y;
                if (textHeight > pageHeight)
                    pageTransform.sizeDelta = new Vector2(0, Mathf.SmoothDamp(pageTransform.sizeDelta.y, pDeltaTarget, ref pDeltaVelocity, 0.5f));
                else if (textHeight < pageHeight)
                    pageTransform.sizeDelta = new Vector2(0, pDeltaTarget);
            }
            else
            {
                pageTransform.sizeDelta = new Vector2(0, 0);
            }
        }
        pageHeight = pageTransform.rect.height;
        textHeight = textTransform.sizeDelta.y;

        if (autoScroll)
        {
            targetPos = (textHeight / 2f) - (pageHeight / 2f) + offset;

            if (targetPos > 0)
                targetPos = 0;

            if (resized)
            {
                textTransform.anchoredPosition = new Vector2(0, targetPos);
                velocity = 0f;
            }
            else
            {
                textTransform.anchoredPosition = new Vector2(0, Mathf.SmoothDamp(textTransform.anchoredPosition.y, targetPos, ref velocity, smoothTime));
            }
        }
        else
        {
            textTransform.anchoredPosition = new Vector2(0, 0);
        }

        previousParentDimensions = parentDimensions;
    }

    public void InitializeTextPosition()
    {
        if (autoScroll)
        {
            float pageHeight = pageTransform.rect.height;
            targetPos = offset - (pageHeight / 2f);
            textTransform.anchoredPosition = new Vector2(0, targetPos);
            velocity = 0f;
        }
        else
            textTransform.anchoredPosition = new Vector2(0, 0);
        previousParentDimensions = new Vector2(pagesParent.rect.width, pagesParent.rect.height);
    }

    public override void SetTextContent(string str)
    {
        finalText = str;
        activeText = "";
        tagsAppendix = "";
        currentCharacter = 0;
        timeElapsed = 0f;
        status = CascadeTextStatus.Ready;
    }

    public override void PageOpened()
    {
        base.PageOpened();
        StartWriting();
    }

    public void StartWriting()
    {
        if (status == CascadeTextStatus.Ready)
        {
            status = CascadeTextStatus.Writing;
            if (buttonsParent)
                buttonsParent.gameObject.SetActive(false);
            InitializeTextPosition();
        }
    }

    protected void FinishWriting()
    {
        if (status == CascadeTextStatus.Writing)
        {
            status = CascadeTextStatus.Finished;
            if (buttonsParent)
                buttonsParent.gameObject.SetActive(true);
        }
    }

    public void CascadeTextSettings(float charactersPerSecond, bool autoScroll, float smoothTime, IfExceedsPage ifExceedsPage)
    {
        this.charactersPerSecond = charactersPerSecond;
        this.autoScroll = autoScroll;
        this.smoothTime = smoothTime;
        this.ifExceedsPage = ifExceedsPage;
        if (this.ifExceedsPage == IfExceedsPage.BestFitFontSize)
            usePageMask = false;
    }
}
