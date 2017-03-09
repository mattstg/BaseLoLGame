using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class Page : MonoBehaviour
{
    public string nextText = "";
    public bool nextTextOverride = false;
    public bool nextEnabled = true;
    public bool nextActive = true;

    public string backText = "";
    public bool backTextOverride = false;
    public bool backEnabled = true;
    public bool backActive = true;

    public string titleText = "";
    public bool titleTextOverride = false;
    public bool hideTitle = false;

    public bool useScrollRect = false;
    public bool useScrollbar = false;
    public bool usePageMask = true;

    protected PanelManager panelManager;

    public void AssignPanelManager(PanelManager _panelManager)
    {
        panelManager = _panelManager;
    }

    public virtual bool NextRequested()     // Page children can override button behaviour
    {
        return true;                        // return true/false = request permitted/denied
    }

    public virtual bool BackRequested()
    {
        return true;
    }

    public void NextSettings(string text, bool textOverride, bool enabled, bool active)
    {
        nextText = text;
        nextTextOverride = textOverride;
        nextEnabled = enabled;
        nextActive = active;
    }

    public void BackSettings(string text, bool textOverride, bool enabled, bool active)
    {
        backText = text;
        backTextOverride = textOverride;
        backEnabled = enabled;
        backActive = active;
    }

    public void TitleSettings(string text, bool textOverride, bool hide)
    {
        titleText = text;
        titleTextOverride = textOverride;
        hideTitle = hide;
    }

    public void ScrollSettings(bool scrRect, bool scrollbar, bool pageMask)
    {
        useScrollRect = scrRect;
        useScrollbar = scrollbar;
        usePageMask = pageMask;
    }
}
