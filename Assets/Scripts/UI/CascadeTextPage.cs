using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum CascadeTextStatus{Ready, Writing, Finished};
public enum IfExceedsParent{BestFitFontSize, ExtendDownward};

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

    public IfExceedsParent ifExceedsParent = IfExceedsParent.BestFitFontSize;
    public bool autoScroll = true;
    public float smoothTime = 3f;
    public float offset = 0f;
    private float velocity = 0f;
    private float targetPos = 0f;

    private float pDeltaVelocity = 0f;
    


    protected override void Awake()
    {
        image = gameObject.GetComponent<Image>();
        if (textTransform)
            text = textTransform.GetComponent<Text>();
        else
            text = GetComponentInChildren<Text>();
        pageTransform = (RectTransform)transform;
    }

    protected override void Start()
    {
        pageParent = (RectTransform)transform.parent;
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
        if (autoScroll)
            UpdateTextPosition();
        else
            textTransform.anchoredPosition = new Vector2(0, 0);
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
        float parentHeight = pageParent.rect.height;
        float pageHeight = pageTransform.rect.height;
        float textHeight = textTransform.sizeDelta.y;
        float pDeltaTarget = 0f;

        if (textHeight > pageHeight)
        {
            if (ifExceedsParent == IfExceedsParent.BestFitFontSize)
            {
                text.fontSize--;
            }
            else if (ifExceedsParent == IfExceedsParent.ExtendDownward)
            {
                pDeltaTarget = textHeight - parentHeight;
            }
        }

        targetPos = (textHeight / 2f) - (pageHeight / 2f) + offset;

        if (targetPos > 0)
            targetPos = 0;

        textTransform.anchoredPosition = new Vector2(0, Mathf.SmoothDamp(textTransform.anchoredPosition.y, targetPos, ref velocity, smoothTime));
        if (ifExceedsParent == IfExceedsParent.ExtendDownward)
            pageTransform.sizeDelta = new Vector2(0, Mathf.SmoothDamp(pageTransform.sizeDelta.y, pDeltaTarget, ref pDeltaVelocity, 0.5f));
        else
            pageTransform.sizeDelta = new Vector2(0, 0);
    }

    public void InitializeTextPosition()
    {
        if (autoScroll)
        {
            float pageHeight = pageTransform.rect.height;
            //float textHeight = textTransform.sizeDelta.y;
            targetPos = 17f - (pageHeight / 2f) + offset;
            textTransform.anchoredPosition = new Vector2(0, targetPos);
            velocity = 0f;
        }
        else
            textTransform.anchoredPosition = new Vector2(0, 0);
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

    public override void AssignPanelManager(PanelManager _panelManager)
    {
        base.AssignPanelManager(_panelManager);
        buttonsParent = panelManager.buttonsParent;
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
}
