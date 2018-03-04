using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoundInfo : MonoBehaviour {

    public Text roundCountText;

    public Text LeftRescued;
    public Text LeftEaten;

    public Text RightRescued;
    public Text RightEaten;

    // Use this for initialization
    void Start () {
        roundCountText.text += Rounds.Instance.CurrentRound;
        LeftRescued.text = "Moutons sauvés : " + Rounds.Instance.TeamOne.NBSheepRescued;
        LeftEaten.text = "Moutons mangés : " + Rounds.Instance.TeamOne.NbSheepEaten;

        RightRescued.text = Rounds.Instance.TeamTwo.NBSheepRescued + " : Moutons sauvés";
        RightEaten.text = Rounds.Instance.TeamTwo.NbSheepEaten + " : Moutons mangés";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
