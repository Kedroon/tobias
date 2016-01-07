using UnityEngine;
using System.Collections;

public class FoodManager : MonoBehaviour {

	private IEnumerator coroutinespeed;
	public static float speedfood;
	public static float speedfoodlow;
	public static float perc;

	public static float speedscrool;

	// Use this for initialization
	void Awake () {
	
		coroutinespeed = Increasespeed ();
		speedfood = 0.4f;
		speedfoodlow = 0.32f;
		speedscrool = 2;
		perc = 1f;

	}

	public void Iniciarcoroutine(){
		StartCoroutine (coroutinespeed);
	
	}

	public void Pararcoroutine(){
		StopCoroutine (coroutinespeed);
	}
	
	// Update is called once per frame
	void Update () {

		if (perc >= 2.2f) {
			Pararcoroutine();
		}
	
	}


	IEnumerator Increasespeed()
	{
		
		while (true) {
			
			yield return new WaitForSeconds (14);
			perc+=0.15f;
			speedscrool=2f*perc;
			speedfood=0.4f/perc;
			speedfoodlow = 0.32f / perc;

			
			
			
			
		}
		
	}
}
