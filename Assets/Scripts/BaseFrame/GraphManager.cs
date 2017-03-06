using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour {

	GameFlow gameflow;

	string slotmbPrefab = "Prefabs/UI/SlotMB";

    public CanvasManager canvasManger;
	public Transform grid;
	public Button gridButton;
	public Text titleText;

	public bool allCorrect = false;
    public bool hasEvaluated = false;

    Text buttonText;
    int tries = 0;
    float totalScore = 0;

    public void Awake()
    {
        gameflow = GameObject.FindObjectOfType<GameFlow>();
        buttonText = gridButton.GetComponentInChildren<Text>();
    }

    public void Start()
    {
        SetButtonEnabled(false);
    }

	public void ResetGraphManager()
	{
		allCorrect = false;
		foreach (Transform t in grid)
			Destroy (t.gameObject);
        SetButtonEnabled(false);
	}

	public void GridButtonPressed ()
	{
		if (!allCorrect)
        {
			
            if (!hasEvaluated)
            {
                tries++;
                int questions = 0;
                int correct = 0;
                foreach (Transform t in grid)
                {
                    questions++;
                    SlotMB slotmb = t.GetComponent<SlotMB>();
                    if (slotmb.GetIsCorrect())
                    {
                        slotmb.QuestionIsCorrect();
                        correct++;
                    }
                    else
                    {
                        slotmb.DisplayIncorrectImage();
                    }
                }
                if (questions == correct)
                {
                    canvasManger.PlayAudio(UISoundEffect.Right);
                    allCorrect = true;
                    gridButton.GetComponentInChildren<Text>().text = "Perfect!";
                    totalScore += 1;
                }
                else
                {
                    canvasManger.PlayAudio(UISoundEffect.Wrong);
                    gridButton.GetComponentInChildren<Text>().text = "Try Again";
                    totalScore += (float)correct / (float)questions;
                }
                hasEvaluated = true;
            }
            else  //if (hasEvaluated)
            {
                foreach (Transform t in grid)
                {
                    SlotMB slotmb = t.GetComponent<SlotMB>();
                    if (slotmb.GetIsCorrect())
                    {
                        continue;
                    }
                    else
                    {
                        slotmb.ClosePopupPressed();
                    }
                }
                gridButton.GetComponentInChildren<Text>().text = "Submit";
                hasEvaluated = false;
            }
		} 
		else 
		{
            int roundNumber = GetRoundNumber();
            if (roundNumber != -1)
            {
                int progressNumber = roundNumber * 2 + 2;
                ProgressTracker.Instance.SetRoundMult((float)totalScore / (float)tries, GetRoundNumber(), tries - 1);
                ProgressTracker.Instance.SubmitProgress(progressNumber);
            }
			gameflow.AnsweredGraphCorrectly ();
		}

//		Debug.Log (string.Format ("Score is {0}/{1}", correct, questions));
	}

    private int GetRoundNumber()
    {
        switch(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "FrogScene":
                return 0;
            case "FishScene":
                return 1;
            case "BowerScene":
                return 2;
            case "DuckScene":
                return 3;
            case "CaribouScene":
                return 4;
            case "IntroScene":
            case "FinalScene":
                return -1;
            default:
                Debug.Log("scene failure");
                return 0;
        }
    }

	public void AddSlot(Slot slot)
	{
		GameObject go = Instantiate (Resources.Load (slotmbPrefab)) as GameObject;
        SlotMB slotMB = go.GetComponent<SlotMB>();
        slotMB.SetGraphManager(this);
		slotMB.InitializeSlot (slot);
		go.transform.SetParent (grid);
	}

    public void SetButtonEnabled(bool isEnabled)
    {
        if (gridButton.interactable != isEnabled)
        {
            if (isEnabled)
            {
                gridButton.interactable = true;
                Color c = buttonText.color;
                c.a = 1;
                buttonText.color = c;
            }
            else
            {
                gridButton.interactable = false;
                Color c = buttonText.color;
                c.a = .5f;
                buttonText.color = c;
            }
        }
    }
}
