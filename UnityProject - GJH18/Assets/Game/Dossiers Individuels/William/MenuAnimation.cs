using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour {

	void Start () {
        GetComponent<Animator>().SetBool("running", true);
	}
	
}
