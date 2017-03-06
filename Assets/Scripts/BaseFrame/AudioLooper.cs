using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class AudioLooper {

    private string audioName;
    private float trackLength;
    private bool isPlaying = false;
    float[] curTime = new float[2];
    bool[] playingAudio = new bool[2];
    private float fadeTime = 3;

    public void StartAudioLooper(LessonType lessonType)
    {
        if (lessonType != LessonType.Intro && lessonType != LessonType.Post) //they have music, not ambience
        {
            curTime[0] = curTime[1] = 0;
            playingAudio[0] = playingAudio[1] = false;
            trackLength = GetLengthOfAmbient(lessonType);
            audioName = lessonType + "Ambient.mp3";
            isPlaying = true;
            PlayAudio(0);
        }
    }

    public void Update()
    {
        if(isPlaying)
        {
            for(int i = 0; i < 2; i++)
            {
                if(playingAudio[i])
                {
                    curTime[i] += Time.deltaTime;
                    if(!playingAudio[GOI(i)] && curTime[i] + fadeTime >= trackLength)
                    {
                        PlayAudio(GOI(i));
                    }
                    if(curTime[i] >= trackLength)
                    {
                        curTime[i] = 0;
                        playingAudio[i] = false;
                    }
                }
            }
        }
    }

    private void PlayAudio(int i)
    {
        curTime[i] = 0;
        playingAudio[i] = true;
        LOLAudio.Instance.PlayAudio(audioName, false);
    }

    public void CloseAudioLooper()
    {
        LOLAudio.Instance.StopAudio(audioName);
        LOLAudio.Instance.StopAudio(audioName);
    }


    private float GetLengthOfAmbient(LessonType lessonType)
    {
        switch (lessonType)
        {
            case LessonType.Frog:
                return 13;
            case LessonType.Fish:
                return 27;
            case LessonType.Bower:
                return 33;
            case LessonType.Duck:
                return 36;
            case LessonType.Caribou:
                return 8;
        }
        return 0;
    }

    private int GOI(int i) //Get Other Index
    {
        return (i + 1) % 2;
    }
    
}
