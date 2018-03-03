using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfTimer : MonoBehaviour {

    public Text counter;

    private int currentValue;
    private float currentSecondAmount = 0;

	public void SetCounterValue(int value)
    {
        currentValue = value;
        counter.text = "" + currentValue;
        currentSecondAmount = 0;
    }

    void Update()
    {
        currentSecondAmount += Time.deltaTime;
        if(currentSecondAmount > 1)
            SetCounterValue(--currentValue);
    }
}
