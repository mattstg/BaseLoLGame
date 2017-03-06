using UnityEngine; using LoLSDK;
using UnityEngine.UI;
using System;
using System.Collections;


public class TextPanel : MonoBehaviour {

	GameFlow gameflow;
	public Button nextButton;
    RectTransform rectTransform;

    public Text text;
    string completeText = "";
	string activeText;
	string closeMarkup;
    float lettersPerSecond = 60f;
	public int fontSize = 26;
    bool started = false;
    int curLetter;
    float timeBanked = 0;

	private float velocity = 0f;
	public float dampTime = 3f;
	private float targetPos = 0f;
	private float oldPanelHeight = 0f;
	private float oldTextHeight = 0f;
	private float margin = 80f;
	public float offset = 4f;

    public void Awake()
    {
        gameflow = GameObject.FindObjectOfType<GameFlow>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void SetText(string _completeText)
    {
        completeText = _completeText;
		activeText = "";
		closeMarkup = "";
        curLetter = 0;
        text.text = "";
        started = false;
        timeBanked = 0;
    }

    public void StartWriting()
    {
		nextButton.gameObject.SetActive(false);
        started = true;
		InitTextPosition();
    }
		
    public void Update()
    {
		if (!started && velocity < .5f && velocity > -.5
			&& targetPos < (text.rectTransform.anchoredPosition.y + 1)
			&& targetPos > (text.rectTransform.anchoredPosition.y - 1)
			&& rectTransform.rect.height == oldPanelHeight
			&& text.rectTransform.sizeDelta.y == oldTextHeight)
            return;

        timeBanked += Time.deltaTime;

		if (curLetter < completeText.Length)
        {
            while (timeBanked >= 1 / lettersPerSecond && curLetter < completeText.Length)
            {
				while (new String(completeText[curLetter], 1) == "<")		// this section, and methods MarkupOpen/Close, handle markup with running text
				{
					activeText += completeText[curLetter++];			// <
					if (new String(completeText[curLetter], 1) == "/")
					{
						activeText += completeText[curLetter++];		// /
						activeText += completeText[curLetter++];		// b
						activeText += completeText[curLetter++];		// >
						MarkupClose ();
					}
					else
					{
						MarkupOpen (completeText[curLetter]);						
						activeText += completeText[curLetter++];		// b
						activeText += completeText[curLetter++];		// >
					}
				}

				timeBanked -= (1 / lettersPerSecond);
				activeText += completeText[curLetter++];
				text.text = activeText + closeMarkup;
            }
        }
        else
        {
            started = false; //reached end
			nextButton.gameObject.SetActive(true);
        }
		SetTextPosition();
    }

	public void SetTextPosition()
	{
		float panelHeight = rectTransform.rect.height;
		float textHeight = text.rectTransform.sizeDelta.y;
		float topMargin = panelHeight - margin;
		float bottomMargin = margin;		//preventing overlap with button

		if (textHeight > (topMargin - bottomMargin))
			text.fontSize --;
        if (textHeight < (topMargin - bottomMargin - 100) && text.fontSize < 26)    // -100 to prevent oscillations
            text.fontSize++;

        targetPos = (textHeight / 2) - (((topMargin - bottomMargin) / 2) + margin) + offset;

		if (targetPos > offset - margin)
			targetPos = offset - margin;

		text.rectTransform.anchoredPosition = new Vector2(0, Mathf.SmoothDamp(text.rectTransform.anchoredPosition.y, targetPos, ref velocity, dampTime));

		//Debug.Log ("panelHeight " + panelHeight + "  textHeight " + textHeight + "  topMargin " + topMargin + "  bottomMargin " + bottomMargin + "  targetPos " + targetPos + "  anchoredPos.y " + text.rectTransform.anchoredPosition.y + "  velocity " + velocity);

		oldPanelHeight = panelHeight;
		oldTextHeight = textHeight;
	}

	public void InitTextPosition()
	{
		float panelHeight = rectTransform.rect.height;
		float topMargin = panelHeight - margin;
		float bottomMargin = margin;
		targetPos = 20 - (((topMargin - bottomMargin) / 2) + margin) + offset;
		text.rectTransform.anchoredPosition = new Vector2(0, targetPos);
		oldPanelHeight = panelHeight;
		oldTextHeight = 0f;
		velocity = 0f;
		text.fontSize = fontSize;
	}

	public void MarkupOpen(char letter)
	{
		closeMarkup = "<" + "/" + letter + ">" + closeMarkup;
	}

	public void MarkupClose()
	{
		if (!String.Equals(closeMarkup, ""))
		{
			int length = closeMarkup.Length;
			int i = 0;
			while (new String(closeMarkup[i], 1) != ">")
			{
				i++;
			}
			i++;
			length -= i;
			closeMarkup = new string(closeMarkup.ToCharArray(i, length));
		}
	}

	public void NextPressed()
	{
		gameflow.TextButtonNextPressed ();
	}

}
