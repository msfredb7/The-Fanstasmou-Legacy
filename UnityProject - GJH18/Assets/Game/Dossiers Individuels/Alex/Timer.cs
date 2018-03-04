using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour {

    public Text text;
    public int initialTime;
    public bool timerStarted = false;

    private int currentValue = 0;
    private float deltaTimeTimer = 0;

	public void Init(int initialTime)
    {
        this.initialTime = initialTime;
        currentValue = initialTime;
        deltaTimeTimer = 0;
        text.DOFade(1, 1).OnComplete(delegate() {
            timerStarted = true;
        });
    }
	
	void Update ()
    {
        if (timerStarted)
        {
            deltaTimeTimer += Time.deltaTime;
            if(deltaTimeTimer >= 1)
            {
                currentValue--;
                deltaTimeTimer = 0;
            }
            text.text = "" + currentValue;
            if (currentValue <= 0)
            {
                timerStarted = false;
                GetComponent<FlashColor>().Flash();
            }
        }
	}
}
