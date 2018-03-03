using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlashColor : MonoBehaviour {

    public Color newColor;
    public float animDuration = 1;

    public enum ComponentType { noFade = 0, Text = 1, Image = 2 }
    public ComponentType currentType = ComponentType.noFade;

    public bool onStart = true;

    void Start()
    {
        if (onStart)
            Flash();
    }

    void Flash()
    {
        switch (currentType)
        {

            default:
            case ComponentType.noFade:
                break;
            case ComponentType.Text:
                Sequence sqc1 = DOTween.Sequence();
                sqc1.Append(GetComponent<Text>().DOColor(newColor, animDuration));
                sqc1.SetLoops(-1, LoopType.Yoyo);
                break;
            case ComponentType.Image:
                Sequence sqc2 = DOTween.Sequence();
                sqc2.Append(GetComponent<Image>().DOColor(newColor, animDuration));
                sqc2.SetLoops(-1, LoopType.Yoyo);
                break;
        }
    }
}
