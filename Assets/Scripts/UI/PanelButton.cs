using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PanelButton : CanvasButton
{
    protected PanelManager panelManager;

    public void AssignPanelManager(PanelManager _panelManager)
    {
        panelManager = _panelManager;
    }

    protected override void WasClicked(bool directAction)
    {
        if (panelManager)
            panelManager.ButtonClicked(this);
    }

    protected override void BecameSelected(bool directAction)
    {
        if (panelManager)
            panelManager.ButtonSelected(this);
    }

    protected override void BecameDeselected(bool directAction)
    {
        if (panelManager)
            panelManager.ButtonDeselected(this);
    }
}
