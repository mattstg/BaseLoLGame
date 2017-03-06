using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Slot  {

	public Dictionary<int,SlotInfo> slotInfoDict = new Dictionary<int, SlotInfo> ();
	public string questiontext = "";
	public string questionExplained = "";
	public Slot()
	{

	}

	public void SetQuestion(string _question, string explaination)
	{
		questiontext = _question;
		questionExplained = explaination;
	}

	public void SetAns(int _id, string _text, bool _isCorrect, string _popupText = "")
	{
        if(_popupText == "")
        {
            if(_isCorrect)
            {
                _popupText = "Correct:  " + _text + ".";
            }
            else
            {
                _popupText = "Incorrect";
            }
        }

		SlotInfo si = new SlotInfo (_id, _text, _isCorrect, _popupText);
		if (slotInfoDict.ContainsKey (_id)) {
			slotInfoDict [_id] = si;
		} else {
			slotInfoDict.Add (_id, si);
		}
	}

	public string GetWrongPopup(int pid)
	{
		if (slotInfoDict.ContainsKey (pid))
			return slotInfoDict [pid].popupText;
		return "Please select an answer";
	}

	public bool IsCorrectAns(int qid)
	{
		if (slotInfoDict.ContainsKey (qid))
			return slotInfoDict [qid].isCorrect;
		return false;
	}

	public class SlotInfo
	{
		public int id;
		public string text;
		public bool isCorrect;
		public string popupText;

		public SlotInfo(int _id, string _text, bool _isCorrect, string _popupText)
		{
			id = _id;
			text = _text;
			isCorrect = _isCorrect;
			popupText = _popupText;
		}
	}
}
