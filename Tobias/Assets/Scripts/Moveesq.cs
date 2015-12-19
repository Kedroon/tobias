using UnityEngine;
using System.Collections;

public class Moveesq : MonoBehaviour {

	public float speed;
	private IEnumerator coroutine;

	void Start(){
		coroutine = Voar ();
		StartCoroutine (coroutine);
	}

	void OnEnable(){
		Controller.OnDeath += Parar;
	}

	void OnDisable(){
		Controller.OnDeath -= Parar;
	}
	

	IEnumerator Voar(){
		while (true) {
			gameObject.transform.Translate ( 2f * Time.deltaTime * speed, -0.5f*Time.deltaTime, 0);
			yield return null;
		}

	}

	void Parar(){
		StopCoroutine (coroutine);

	}
}
