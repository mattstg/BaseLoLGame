using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class AspectRatioMatch : MonoBehaviour
{
    public Image image;
    public AspectRatioFitter aspectRF;

    void Start()
    {
        UpdateAspectRatio();
        gameObject.GetComponent<Page>().InitiateScroll();
    }

    public void UpdateAspectRatio()
    {
        if (image && aspectRF)
        {
            if (image.sprite)
            {
                Sprite sprite = image.sprite;
                Rect rect = sprite.rect;
                float aspect = 1f;
                if (rect.height != 0f)
                    aspect = rect.width / rect.height;
                aspectRF.aspectRatio = aspect;
            }
        }
    }
}
