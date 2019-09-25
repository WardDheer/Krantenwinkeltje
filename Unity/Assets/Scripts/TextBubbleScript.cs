using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBubbleScript : MonoBehaviour
{
    public Image TextBubbleImage;
    private Image _lastImage;

    public GameObject Object1;

    private void Update()
    {
        if (TextBubbleImage != _lastImage)
        {
            Object1.GetComponent<Image>().sprite = TextBubbleImage.sprite;
        }

        _lastImage = TextBubbleImage;
    }
}
