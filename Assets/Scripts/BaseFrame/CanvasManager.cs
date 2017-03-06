using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public enum UISoundEffect { Click, Right, Wrong}
public class CanvasManager : MonoBehaviour {

   
    public void PlayAudio(int enumAsInt)
    {
        PlayAudio((UISoundEffect)enumAsInt);
    }

    public void PlayAudio(UISoundEffect soundType)
    {
        switch(soundType)
        {
            case UISoundEffect.Click:
                LOLAudio.Instance.PlayAudio("ClickNoise.wav", false);
                break;
            case UISoundEffect.Right:
                LOLAudio.Instance.PlayAudio("correct.wav", false);
                break;
            case UISoundEffect.Wrong:
                LOLAudio.Instance.PlayAudio("wrong.wav", false);
                break;
        }
    }
}
