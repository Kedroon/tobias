using UnityEngine;
using System.Collections;

public class TriggerDelete : MonoBehaviour {



	void Start(){
	}

	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.CompareTag("Trigger")) {
			DestroyObject (gameObject);
		}
				
}
}