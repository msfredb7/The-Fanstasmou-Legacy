using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ButtonPopUp : MonoBehaviour {

    public GameObject buttonA;
    public GameObject buttonB;
    public GameObject buttonX;
    public GameObject buttonY;
    public GameObject buttonTrigger;

    bool AWaitForFocusComplete = false;
    bool BWaitForFocusComplete = false;
    bool XWaitForFocusComplete = false;
    bool YWaitForFocusComplete = false;
    bool TriggerWaitForFocusComplete = true;

    public float fadeDuration = 1;

    public enum ButtonType
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3,
        Trigger =4
    }

    void Start ()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        buttonX.SetActive(false);
        buttonY.SetActive(false);
        buttonTrigger.SetActive(false);
    }
	
	public void FocusPopupOnPosition(Vector2 uiPos, ButtonType buttonType, float duration, string text)
    {
        switch (buttonType)
        {
            default:
            case ButtonType.A:
                if (AWaitForFocusComplete)
                    break;
                AWaitForFocusComplete = true;
                FocusPopup(uiPos, buttonA, duration, text,()=> { AWaitForFocusComplete = false; });
                break;
            case ButtonType.B:
                if (BWaitForFocusComplete)
                    break;
                BWaitForFocusComplete = true;
                FocusPopup(uiPos, buttonB, duration, text, () => { AWaitForFocusComplete = false; });
                break;
            case ButtonType.X:
                if (XWaitForFocusComplete)
                    break;
                XWaitForFocusComplete = true;
                FocusPopup(uiPos, buttonX, duration, text, () => { AWaitForFocusComplete = false; });
                break;
            case ButtonType.Y:
                if (YWaitForFocusComplete)
                    break;
                YWaitForFocusComplete = true;
                FocusPopup(uiPos, buttonY, duration, text, () => { AWaitForFocusComplete = false; });
                break;
            case ButtonType.Trigger:
                if (TriggerWaitForFocusComplete)
                    break;
                TriggerWaitForFocusComplete = true;
                FocusPopup(uiPos, buttonTrigger, duration, text, () => { TriggerWaitForFocusComplete = false; });
                break;
        }
    }

    private void FocusPopup(Vector2 uiPos, GameObject button, float duration, string text,Action onComplete)
    {
        button.transform.position = new Vector3(uiPos.x, uiPos.y, 0);
        button.GetComponentInChildren<Text>().text = text;
        button.GetComponent<Image>().SetAlpha(0);
        button.GetComponentInChildren<Text>().color = button.GetComponentInChildren<Text>().color.ChangedAlpha(0);
        button.SetActive(true);
        Sequence sqc = DOTween.Sequence();
        sqc.Join(button.GetComponent<Image>().DOFade(0.5f, fadeDuration));
        sqc.Join(button.GetComponentInChildren<Text>().DOFade(0.5f, fadeDuration));
        this.DelayedCall(delegate ()
        {
            Sequence sqc2 = DOTween.Sequence();
            sqc2.Join(button.GetComponentInChildren<Text>().DOFade(0, fadeDuration));
            sqc2.Join(button.GetComponent<Image>().DOFade(0, fadeDuration));
            sqc2.OnComplete(delegate() {
                onComplete();
                button.SetActive(false);
            });
        }, duration);
    }
}
