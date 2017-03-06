using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using System.Linq;

public class ProgressTracker {

	private static ProgressTracker instance;

	public static ProgressTracker Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new ProgressTracker();
			}
			return instance;
		}
	}

	float[] roundScores = new float[5];
	float[] roundMult = new float[5];
    float maxScorePerRound = 1500;
    float roundMultMax = .5f; //50% increase at 100% bonus
    float lossPerQuizAttempt = .05f;
    bool trackProgress = true;

	private ProgressTracker()
	{
		for (int i = 0; i < 5; i++) 
		{
			roundScores [i] = 0;
			roundMult [i] = 0;
		}
	}

    public float GetRoundScore(LessonType lesson)
    {
        float score = roundScores[(int)lesson] * maxScorePerRound;
        int toInt = (int)score;
        score = (float)toInt;
        return score;
    }

    public float GetMultScore(LessonType lesson)
    {
        float bonus = 1 + roundMult[(int)lesson]*roundMultMax;

        //round down to nearest percentage
        bonus *= 100f;
        int toInt = (int)bonus;
        bonus = ((float)toInt) / 100f;

        return bonus;
    }

    public float GetMultPercentage(LessonType lesson)
    {
        float bonus = roundMult[(int)lesson] * roundMultMax * 100f;
        return (float)(int)bonus;
    }

    public void SetRoundScore(float score, int round)
	{
		roundScores [round] = Mathf.Clamp(score,0,1);
	}

	public void SetRoundMult(float score, int round, int tries)
	{
        //float _scooore = score;
        score = Mathf.Clamp(score - (tries * lossPerQuizAttempt), 0,1);
        //Debug.Log("raw score: " + _scooore + " for round: " + round + " took " + tries + " tries, resulting in final score: " + score);
        roundMult [round] = score;
	}

	public void SubmitProgress(int progressNumber)
	{
        if(trackProgress)
            LOLSDK.Instance.SubmitProgress(GetTotalScore(), progressNumber, 10);// SCORE, CURRENTPROGRESS, MAXPROGRESS
    }

    public int GetTotalScore()
    {
        float totalScore = 0;
        for (int i = 0; i < 5; i++)
        {
            //totalScore += roundScores[i] * (1 + roundMult[i] * roundMultMax) * maxScorePerRound;
            totalScore += GetRoundScore((LessonType)i) * GetMultScore((LessonType)i);
            //Debug.Log(string.Format("i: {0}, TotalScore: {1}, GetRoundScore: {2}, GetMultScore: {3}, Score[i]: {4}", i, (int)totalScore, GetRoundScore((LessonType)i), GetMultScore((LessonType)i), GetRoundScore((LessonType)i) * GetMultScore((LessonType)i)));
        }
        return (int)totalScore;
    }
}
