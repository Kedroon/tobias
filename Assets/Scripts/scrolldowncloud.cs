using UnityEngine;
using System.Collections;

public class scrolldowncloud : MonoBehaviour {
	public float speed;
   
	// Use this for initialization
	void Start () {
		speed = 0.2f;
		StartCoroutine (MoveClouds ());
    }

	



	IEnumerator MoveClouds(){
		while (transform.position.x>-8) {
			transform.Translate (-1 * Time.deltaTime * speed, 0, 0);
			yield return null;
		}

			Destroy(gameObject);

	}


}