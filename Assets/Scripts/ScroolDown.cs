using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScroolDown : MonoBehaviour {

	public float speedscrool;
	private bool sopro;
	private Vector3 targetposition;
	public float speed;
	private EnemyIA inimigo;
	private string originaltag;
	private IEnumerator cair;


	void Awake () {

		sopro = false;
		originaltag = tag;
		cair = Cair ();

	}

	void Start(){
		StartCoroutine (cair);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("TriggerChupa")) {
			tag=("ChupaFood");
			sopro = true;
			targetposition = other.transform.position;
			inimigo = other.GetComponentInParent<EnemyIA>();

	


		}
	}

	void OnEnable(){
		Controller.OnDeath += Parar;
	}

	void OnDisable(){
		Controller.OnDeath -= Parar;
	}

	void Parar(){
		StopCoroutine (cair);
	}


   

	public IEnumerator Cair(){
		while (true) {
			if(!sopro){
				transform.Translate (0.0f, -1 * Time.deltaTime * FoodManager.speedscrool, 0);

			}
			else{
				transform.position = Vector3.MoveTowards(transform.position, targetposition, speed * Time.deltaTime);
				if(!inimigo.chupar){
					tag=originaltag;
					sopro=false;
				}
			}
			yield return null;
		}


	}


}
