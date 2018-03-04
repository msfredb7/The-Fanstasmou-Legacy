using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginRoundInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text += Rounds.Instance.CurrentRound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
