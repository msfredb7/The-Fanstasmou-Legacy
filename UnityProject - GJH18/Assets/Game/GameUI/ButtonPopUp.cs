using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonPopUp : MonoBehaviour {

    public GameObject buttonA;
    public GameObject buttonB;
    public GameObject buttonX;
    public GameObject buttonY;

    public float fadeDuration = 1;

    public enum ButtonType
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3
    }

    void Start ()
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        buttonX.SetActive(false);
        buttonY.SetActive(false);
    }
	
	public void FocusPopupOnPosition(Vector2 uiPos, ButtonType buttonType, float duration, string text)
    {
        switch (buttonType)
        {
            default:
            case ButtonType.A:
                FocusPopup(uiPos, buttonA, duration, text);
                break;
            case ButtonType.B:
                FocusPopup(uiPos, buttonB, duration, text);
                break;
            case ButtonType.X:
                FocusPopup(uiPos, buttonX, duration, text);
                break;
            case ButtonType.Y:
                FocusPopup(uiPos, buttonY, duration, text);
                break;
        }
    }

    private void FocusPopup(Vector2 uiPos, GameObject button, float duration, string text)
    {
        button.transform.position = new Vector3(uiPos.x, uiPos.y, 0);
        button.GetComponentInChildren<Text>().text = text;
        button.GetComponent<Image>().SetAlpha(0);
        button.SetActive(true);
        button.GetComponent<Image>().DOFade(1, fadeDuration);
        this.DelayedCall(delegate ()
        {
            button.GetComponent<Image>().DOFade(0, fadeDuration).OnComplete(delegate() {
                button.SetActive(false);
            });
        }, duration);
    }
}
