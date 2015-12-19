using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	private EnemyIA[] enemyIA;
	public GameObject[] enemies;

	public void ZerarContador(){

		enemyIA = GetComponentsInChildren<EnemyIA>();
		foreach (EnemyIA enemy in enemyIA) {
			enemy.CountComida=0;
		}
	}

	public void InstantiateEnemy(int ID, LineRenderer LineofDeath){
		GameObject inimigosf = Instantiate (enemies[ID]) as GameObject;
		inimigosf.transform.SetParent (gameObject.transform);
		inimigosf.GetComponent<EnemyIA> ().LineofDeath = LineofDeath;

	}




}
