using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class LOLAudio
{
#region Singleton
    private static LOLAudio instance;

    public static LOLAudio Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LOLAudio();
            }
            return instance;
        }
    }
    #endregion
    
    private LOLAudio()
    {
    }

    public void PlayAudio(string _name, bool loop = false)
    {
        if (!MainMenu.Sound_Active || !LOLSDK.Instance.IsInitialized)
            return;

        LOLSDK.Instance.PlaySound(_name, false, loop);
    }

    public void StopAudio(string _name)
    {
        LOLSDK.Instance.StopSound(_name);
    }
}