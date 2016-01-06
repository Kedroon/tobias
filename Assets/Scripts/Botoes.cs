using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Botoes : MonoBehaviour
{

	// Use this for initialization

	
	// Update is called once per frame
	public void Play ()
	{
		SceneManager.LoadScene ("Main"); 

	}

	public void Update ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

}
