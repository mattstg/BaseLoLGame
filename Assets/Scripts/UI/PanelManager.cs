using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PanelManager : MonoBehaviour
{
    public PanelButton nextButton;
    public PanelButton backButton;

    void Awake()
    {

    }

    void Start()
    {
        if (nextButton)
        {
            nextButton.AssignPanelManager(this);
            nextButton.SetMode(false, false);
            nextButton.InitializeState(false, true);
            nextButton.SetText("Next");
            nextButton.SetActive(true);
        }
        if (backButton)
        {
            backButton.AssignPanelManager(this);
            backButton.SetMode(false, false);
            backButton.InitializeState(false, true);
            backButton.SetText("Back");
            backButton.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void ButtonClicked(PanelButton button)
    {
        if (button == nextButton)
        {
            
        }
        else if (button == backButton)
        {
            
        }
        else
        {

        }
    }

    public void ButtonSelected(PanelButton button)
    {

    }

    public void ButtonDeselected(PanelButton button)
    {

    }
}
