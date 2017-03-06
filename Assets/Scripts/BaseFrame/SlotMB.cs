using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;
using UnityEngine.UI;

public class SlotMB : MonoBehaviour {

    public GraphManager graphManager;

    public Sprite unpressedGraphic;
	public Sprite pressedGraphic;

    public Sprite infoDefault;
    public Sprite infoSelected;
    public Sprite infoCorrect;
    public Sprite infoIncorrect;
    public Button infoButton;

    Slot slot;
	public List<Button> buttons = new List<Button> ();

    public GameObject panelQuestion;
    public GameObject panelInfo;
    public GameObject panelB1;
    public GameObject panelB2;
    public GameObject panelB3;
    public GameObject panelB4;
    public Image panelImage;

	int selectedAns = 0;
    bool isExplaining = false;
    bool isEvaluating = false;

    int scrWidth = -1;
    int margins = 80;
    int answersTotal = 0;

    public void InitializeSlot(Slot _slot)
	{
        scrWidth = Screen.width;
        slot = _slot;
		buttons [0].GetComponentInChildren<Text> ().text = _slot.questiontext;
        foreach (KeyValuePair<int, Slot.SlotInfo> kv in slot.slotInfoDict)
            answersTotal++;
        RefreshSlot();
        AdjustPanelWidths();
	}

    public void SetGraphManager(GraphManager gm)
    {
        graphManager = gm;
    }

    public void Update()
    {
        if(scrWidth != Screen.width)
        {
            scrWidth = Screen.width;
            AdjustPanelWidths();
        }

    }

    public void AdjustPanelWidths()
    {
        int total = scrWidth - margins - 52;
        float ansProportion = 0.7F;
        int answerNum = 4;                 //maintains consistent spacing
      //int answerNum = answersTotal;      //stretches available answers horizontally
        int answerWidth = Mathf.FloorToInt(((total * ansProportion) / answerNum) / 2) * 2;
        int totalQ = total - (answerWidth * answerNum);
        int questionWidth = Mathf.FloorToInt(totalQ / 2) * 2;
        int imageWidth = Mathf.FloorToInt((total - questionWidth) / 2) * 2;

        var questionRT = panelQuestion.transform as RectTransform;
        var infoRT = panelInfo.transform as RectTransform;
        var b1RT = panelB1.transform as RectTransform;
        var b2RT = panelB2.transform as RectTransform;
        var b3RT = panelB3.transform as RectTransform;
        var b4RT = panelB4.transform as RectTransform;
        var imageRT = panelImage.transform as RectTransform;

        infoRT.sizeDelta = new Vector2(52, infoRT.sizeDelta.y);
        b1RT.sizeDelta = new Vector2(answerWidth, b1RT.sizeDelta.y);
        b2RT.sizeDelta = new Vector2(answerWidth, b2RT.sizeDelta.y);
        b3RT.sizeDelta = new Vector2(answerWidth, b3RT.sizeDelta.y);
        b4RT.sizeDelta = new Vector2(answerWidth, b4RT.sizeDelta.y);
        questionRT.sizeDelta = new Vector2(questionWidth, questionRT.sizeDelta.y);
        imageRT.sizeDelta = new Vector2(imageWidth, imageRT.sizeDelta.y);
    }

    public void SetIsExplaining(bool isExp)
    {
        if (isExp)
        {
            isExplaining = true;
            infoButton.GetComponent<Image>().sprite = infoSelected;
        }
        else
        {
            isExplaining = false;
            infoButton.GetComponent<Image>().sprite = infoDefault;
        }
    }

    public void SetIsEvaluating(bool isEval)
    {
        if (isEval)
        {
            isEvaluating = true;
            if (GetIsCorrect())
            {
                infoButton.interactable = false;
                infoButton.GetComponent<Image>().sprite = infoCorrect;
            }
            else
            {
                infoButton.interactable = false;
                infoButton.GetComponent<Image>().sprite = infoIncorrect;
            }
        }
        else if (!isEval)
        {
            isEvaluating = false;
            infoButton.interactable = true;
            infoButton.GetComponent<Image>().sprite = infoDefault;
        }
    }

    public bool GetIsCorrect()
	{
		return slot.IsCorrectAns (selectedAns);
	}

	public void DisplayIncorrectImage()
	{
		foreach (Button b in buttons)
            b.transform.parent.gameObject.SetActive(false);
            //b.gameObject.SetActive (false);
        buttons[0].transform.parent.gameObject.SetActive(true);
        panelImage.gameObject.SetActive (true);
		panelImage.GetComponentInChildren<Text> ().text = slot.GetWrongPopup (selectedAns);
        SetIsExplaining(false);
        SetIsEvaluating(true);
	}

	public void RefreshSlot()
	{
		buttons [0].gameObject.SetActive (true);
		foreach (KeyValuePair<int,Slot.SlotInfo> kv in slot.slotInfoDict) {
			Button but = buttons [kv.Key];
			but.GetComponentInChildren<Text> ().text = kv.Value.text;
            but.transform.parent.gameObject.SetActive(true);
            //but.gameObject.SetActive (true);
		}
        SetIsExplaining(false);
        SetIsEvaluating(false);
    }

	public void ClosePopupPressed()
	{
        panelImage.gameObject.SetActive(false);
        RefreshSlot();
	}

	public void ButtonPressed(int butID)
	{
		if (butID == 0 )
        {
            if (isEvaluating)
                return;
            if (!isExplaining)
            {
                QuestionExplainedPressed();
                return;
            }
            else
            {
                ClosePopupPressed();
                return;
            }
		}

		selectedAns = butID;
		for(int i = 1; i < 5; i++)
        {
			if (selectedAns == i)
				buttons [i].GetComponent<Image> ().sprite = pressedGraphic;
			else
				buttons [i].GetComponent<Image> ().sprite = unpressedGraphic;
		}

        graphManager.SetButtonEnabled(true);
	}

	public void QuestionIsCorrect()
	{
		foreach (Button b in buttons)
            b.transform.parent.gameObject.SetActive(false);
            //b.gameObject.SetActive (false);
        buttons[0].transform.parent.gameObject.SetActive(true);
        panelImage.gameObject.SetActive (true);
		panelImage.GetComponentInChildren<Text> ().text = slot.slotInfoDict[selectedAns].popupText;
		/*if(panelImage.GetComponentInChildren<Button> ())
			panelImage.GetComponentInChildren<Button> ().gameObject.SetActive (false);*/
        SetIsExplaining(false);
        SetIsEvaluating(true);
    }

	public void QuestionExplainedPressed()
	{
        foreach (Button b in buttons)
        {
            b.transform.parent.gameObject.SetActive(false);
//            Debug.Log("p name " + b.transform.parent.name);
                }
            //b.gameObject.SetActive (false);
        buttons[0].transform.parent.gameObject.SetActive(true);
		panelImage.gameObject.SetActive (true);
		panelImage.GetComponentInChildren<Text> ().text = slot.questionExplained;
        SetIsExplaining(true);
	}
}
