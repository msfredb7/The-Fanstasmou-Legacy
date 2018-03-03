using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BergerBehavior : MonoBehaviour {

    public enum BergerMode
    {
        Repulse = 0,
        Attract = 1
    }

    public BergerMode currentMode = BergerMode.Repulse;

    public float changeModeCooldown = 0.5f;
    private bool canChange = true;

    private InputPlayerButton inputButtons;
	
    void Start()
    {
        if (inputButtons == null)
            GetInputButtonsRef();
    }

	void Update ()
    {
        if (inputButtons == null)
            GetInputButtonsRef();

        if (inputButtons.GetPlayerA())
        {
            if (!canChange)
            {
                return;
            }
            if(currentMode == BergerMode.Repulse)
            {
                currentMode = BergerMode.Attract;
                Debug.Log("Gentil Chien");
                canChange = false;
                this.DelayedCall(() => { canChange = true; },changeModeCooldown);
            } else
            {
                currentMode = BergerMode.Repulse;
                Debug.Log("Méchant Chien");
                canChange = false;
                this.DelayedCall(() => { canChange = true; }, changeModeCooldown);
            }
        }
    }

    void GetInputButtonsRef()
    {
        inputButtons = GetComponentInParent<InputPlayerButton>();
        if (inputButtons == null)
            Debug.Log("wtf doggy");
    }
}
