using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex_TestScript : MonoBehaviour
{
    public ButtonPopUp buttonPopUp;

    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            buttonPopUp.FocusPopupOnPosition(pos1.transform.position,ButtonPopUp.ButtonType.A,2,"ATTACK");
        } else if(Input.GetKeyDown(KeyCode.S))
        {
            buttonPopUp.FocusPopupOnPosition(pos2.transform.position, ButtonPopUp.ButtonType.A, 2, "DEFEND");
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            buttonPopUp.FocusPopupOnPosition(pos3.transform.position, ButtonPopUp.ButtonType.A, 2, "RUNAWAY");
        }
    }
}