using UnityEngine;
using System.Collections;

public class Botoes : MonoBehaviour {

	// Use this for initialization

	
	// Update is called once per frame
	public void Play () {
		Application.LoadLevel ("Main"); 

	}

	public void Update(){
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit();
		}
	}

}
