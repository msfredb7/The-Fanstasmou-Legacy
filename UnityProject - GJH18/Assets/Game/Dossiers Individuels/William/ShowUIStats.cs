using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIStats : MonoBehaviour {

    public bool isLeftTeam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isLeftTeam)
        {
            GetComponent<Text>().text = Rounds.Instance.TeamOne.NBSheepRescued.ToString();
        }
        else
        {
            GetComponent<Text>().text = Rounds.Instance.TeamTwo.NBSheepRescued.ToString();
        }
	}
}
