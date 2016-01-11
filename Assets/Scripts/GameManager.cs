using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

	public float columns = 2.5f;
	public GameObject[] food;
	public GameObject[] cloud;
	public GameObject obs;
	private List <Vector3> gridPositionsfood = new List <Vector3> ();
	private List <Vector3> gridPositionscloud = new List <Vector3> ();
	private List <Vector3> gridPositionsObs = new List <Vector3> ();
	public Transform cenario;
	public Transform clouds;
	public Transform obstaculosesq;
	public Transform obstaculosdir;
	private IEnumerator coroutinefood;
	private IEnumerator coroutinecloud;
	private IEnumerator coroutineobs;
	private IEnumerator coroutineobs1;
	private bool stop = false;
	private bool start;
	public Text recordGamerOver;
	public Text pontosGamerOver;
	public Image gamerOverBackGround;
	public Button buttonrestart;
	public GameObject gameover;
	public GameObject gordura;
	public GameObject pontos;
	public GameObject milho;
	public FoodManager foodmanager;

	public delegate void ConsecutiveWins ();

	public static event ConsecutiveWins onConsecutiveWins;



	
	void InitialiseListfood ()
	{

		gridPositionsfood.Clear ();
		

		for (float x = -2.5f; x <= columns; x += 1) {

			gridPositionsfood.Add (new Vector3 (x, 5.2f, 0f));

		}
	}

	void InitialiseListcloud ()
	{
		gridPositionscloud.Clear ();
		gridPositionscloud.Add (new Vector3 (7f, 1.19162f, 0));
		gridPositionscloud.Add (new Vector3 (7f, 3.869162f, 0));
			
	}

	public void InitialiseListobs ()
	{
		
		gridPositionsObs.Clear ();
		
		
		for (float y = -1f; y <= 5f; y += 1.5f) {
			
			gridPositionsObs.Add (new Vector3 (-3.6f, y, -0.1f));
			
		}
		for (float y = -1f; y <= 5f; y += 1.5f) {
			
			gridPositionsObs.Add (new Vector3 (3.6f, y, -0.1f));
			
		}
	}

	Vector3 RandomPosition (int minrand, int maxrand, List<Vector3> grid)
	{
		Vector3 randomPosition = new Vector3 (0, 0, 0);
		int randomIndex = Random.Range (minrand, maxrand);
		randomPosition = grid [randomIndex];
		return randomPosition;
		
		
	}


	
	void Instanciatefood ()
	{

	
		Vector3 randomPosition = RandomPosition (0, gridPositionsfood.Count, gridPositionsfood);
		GameObject toInstantiate = food [Random.Range (0, food.Length)];
		Instanciatewhatever (toInstantiate, randomPosition, cenario);
		
	}

	void Instanciatecloud ()
	{
		Vector3 randomPosition = RandomPosition (0, gridPositionscloud.Count, gridPositionscloud);
		GameObject toInstantiate = cloud [Random.Range (0, cloud.Length)];
		Instanciatewhatever (toInstantiate, randomPosition, clouds);
			
	
	}

	void Instanciateobsesq ()
	{

        
		Vector3 randomPosition = RandomPosition (0, 4, gridPositionsObs);
		Instanciatewhatever (obs, randomPosition, obstaculosesq);
        
		
	}

	void Instanciateobsdir ()
	{
		
		
		Vector3 randomPosition = RandomPosition (5, 9, gridPositionsObs);
        
		GameObject instance = Instanciatewhatever (obs, randomPosition, obstaculosdir);
        
		Vector3 temp = instance.transform.localScale; 
		temp.x *= -1;
		instance.transform.localScale = temp;
		
		
	}

	GameObject Instanciatewhatever (GameObject gameobject, Vector3 randompos, Transform parent)
	{
		GameObject toInstantiate = gameobject;
		toInstantiate.transform.Translate (randompos);
		GameObject instance = Instantiate (toInstantiate, randompos, Quaternion.identity) as GameObject;
		instance.transform.SetParent (parent);
		return instance;

    
	}




	void Awake ()
	{
		InitialiseListfood ();
		InitialiseListcloud ();
		InitialiseListobs ();


		coroutinefood = Delayfood ();
		coroutinecloud = Delaycloud ();
		coroutineobs = Delayobs ();
		coroutineobs1 = Delayobs1 ();
		StartCoroutine (coroutinecloud);


		
		
	}


	
	// Update is called once per frame
	void Update ()
	{
		if (stop != Controller.estate) {
			stop = Controller.estate;
			start = stop;

		}
		if (start) {
			start = false;

			StartCoroutine (coroutineobs);
			StartCoroutine (coroutineobs1);
			StartCoroutine (coroutinefood);
			foodmanager.Iniciarcoroutine ();
		}
		if (!Controller.vida) {
			foodmanager.Pararcoroutine ();
			StopCoroutine (coroutinefood);
			StopCoroutine (coroutineobs);
			StopCoroutine (coroutineobs1);

			if (Input.GetKeyUp (KeyCode.Escape) && gameover.gameObject.activeSelf) {
				SceneManager.LoadScene (0);

			}
		}


	}

	IEnumerator Delayfood ()
	{

		while (true) {
		
			yield return new WaitForSeconds (Random.Range (FoodManager.speedfoodlow, FoodManager.speedfood));

			Instanciatefood ();

		}
		
	}

	IEnumerator Delaycloud ()
	{
		yield return new WaitForSeconds (2.0f);
		Instanciatecloud ();

		while (true) {
			
			yield return new WaitForSeconds (Random.Range (10f, 16f));
			Instanciatecloud ();
			
			
		}
		
	}

	IEnumerator Delayobs ()
	{
		yield return new WaitForSeconds (3.0f);
		if (Controller.posicao.position.x >= 0) {
			Instanciateobsesq ();
		} else {
			Instanciateobsdir ();
		}
		while (true) {

			yield return new WaitForSeconds (Random.Range (3, 7));
			if (Controller.posicao.position.x >= 0) {
				Instanciateobsesq ();
			} else {
				Instanciateobsdir ();

			}
			
		}

		
	}

	IEnumerator Delayobs1 ()
	{

		while (true) {

			yield return new WaitForSeconds (Random.Range (3, 7));
			if (Controller.posicao.position.x >= 0) {
				Instanciateobsesq ();
			} else {
				Instanciateobsdir ();
				
			}
			
		}
		
		
	}

	public IEnumerator GameOver ()
	{
		if (onConsecutiveWins != null) {
			onConsecutiveWins ();
		}
		yield return new WaitForSeconds (3f);
		//Controller.ShowAd ();
		gameover.gameObject.SetActive (true);
		pontos.SetActive (false);
		milho.SetActive (false);
		gordura.SetActive (false);
		if (Controller.feed > Record.record) {
			Record.record = Controller.feed;
			PlayerPrefs.SetInt ("Player Score", Record.record);
			PlayerPrefs.Save ();
		}
		Analytics.CustomEvent ("GameOver", new Dictionary<string,object> {
			{ "Points", Controller.feed }
		});
		Social.ReportScore (Controller.feed, "CgkIxLn4os8TEAIQCQ", (bool success) => {
			// handle success or failure
		});

		recordGamerOver.text = Record.record.ToString ();
		pontosGamerOver.text = Controller.feed.ToString ();



	}
}
