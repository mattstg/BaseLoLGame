using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PanelButton : CanvasButton
{
    protected PanelManager panelManager;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void AssignPanelManager(PanelManager _panelManager)
    {
        panelManager = _panelManager;
    }

    protected override void WasClicked()
    {
        if (panelManager)
            panelManager.ButtonClicked(this);
    }

    protected override void BecameSelected()
    {
        if (panelManager)
            panelManager.ButtonSelected(this);
    }

    protected override void BecameDeselected()
    {
        if (panelManager)
            panelManager.ButtonDeselected(this);
    }
}
