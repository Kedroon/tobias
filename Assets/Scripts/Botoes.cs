using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;


public class Botoes : MonoBehaviour
{

	// Use this for initialization
	void Start(){
		//PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if(success){
				print("yay");
			}
			else{
				print("oh no");
			}
		});


	}

	public void OpenLeaderBoard(){
		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIxLn4os8TEAIQCQ");

	}

	public void OpenAchievements(){
		Social.ShowAchievementsUI();
		//PlayGamesPlatform.Instance.ShowAchievementsUI;
	}
	
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
