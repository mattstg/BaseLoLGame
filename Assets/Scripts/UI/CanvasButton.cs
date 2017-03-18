using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class CanvasButton : MonoBehaviour
{
    public CanvasManager canvasManager;
    public Sprite selectedSprite;
    protected Sprite defaultSprite;
    [HideInInspector]
    public Button button;
    [HideInInspector]
    public Image image;
    [HideInInspector]
    public Text text;

    public bool canSelect = false;
    public bool canToggle = false;  // canToggle = can deselect by clicking when selected
    public bool clickAudio = false;
    protected bool isSelected = false;
    protected bool isEnabled = true;

    void Awake()
    {
        if (!canvasManager & clickAudio)
            canvasManager = GetComponentInParent<CanvasManager>();
        button = gameObject.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
        text = gameObject.GetComponentInChildren<Text>();
        defaultSprite = image.sprite;
        SetMode(canSelect, canToggle);
        InitializeState(isSelected, isEnabled);
    }

    public void SetMode(bool _canSelect, bool _canToggle)
    {
        if (_canToggle && !_canSelect)
            _canSelect = true;
        canSelect = _canSelect;
        canToggle = _canToggle;
    }

    public void InitializeState(bool becomesSelected, bool becomesEnabled)
    {
        if (becomesSelected && !canSelect)
            becomesSelected = false;
        if (becomesSelected)
            image.sprite = selectedSprite;
        else
            image.sprite = defaultSprite;
        isSelected = becomesSelected;
        SetEnabled(becomesEnabled);
    }

    public virtual void OnClick()
    {
        if (canSelect)
        {
            bool becomesSelected = true;
            if (canToggle && isSelected)
                becomesSelected = false;
            SetSelected(becomesSelected, true);
        }
        else
        {
            WasClicked(true);
        }
        if (clickAudio)
            canvasManager.PlayAudio(UISoundEffect.Click);
    }

    public void SetSelected(bool becomesSelected, bool directAction)
    {
        if (isSelected != becomesSelected && canSelect)
        {
            if (becomesSelected)
            {
                image.sprite = selectedSprite;
                isSelected = true;
                BecameSelected(directAction);
            }
            else
            {
                image.sprite = defaultSprite;
                isSelected = false;
                BecameDeselected(directAction);
            }
        }
        if (!canSelect)
        {
            image.sprite = defaultSprite;
            if (isSelected)
            {
                isSelected = false;
                BecameDeselected(directAction);
            }
        }
    }

    protected virtual void WasClicked(bool directAction) { }

    protected virtual void BecameSelected(bool directAction) { }

    protected virtual void BecameDeselected(bool directAction) { }

    public void SetText(string str)
    {
        if (text)
            text.text = str;
    }

    public void SetEnabled(bool becomesEnabled)
    {
        isEnabled = becomesEnabled;
        button.interactable = isEnabled;
        if (text)
        {
            Color newColor = text.color;
            if (isEnabled)
                newColor.a = 1f;
            else
                newColor.a = 0.5f;
            text.color = newColor;
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public string GetText()
    {
        return text.text;
    }
    
    public bool IsSelected()
    {
        return isSelected;
    }

    public bool IsEnabled()
    {
        return isEnabled;
    }
}
