using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Achievement : MonoBehaviour
{




	
	void OnEnable ()
	{
		Controller.onAchievement += TriggerAchievementScore;
		GameManager.onConsecutiveWins += TriggerAchievementConsecutive;
	}

	void OnDisable ()
	{
		Controller.onAchievement -= TriggerAchievementScore;
		GameManager.onConsecutiveWins -= TriggerAchievementConsecutive;
	}

	void TriggerAchievementScore ()
	{


		switch (Controller.feed) {

		case 10:
			{
				
				Social.ReportProgress ("CgkIxLn4os8TEAIQAw", 100.0f, (bool success) => {
					// handle success or failure
				});
				
				break;
			}

		case 30:
			{
				
				Social.ReportProgress ("CgkIxLn4os8TEAIQBA", 100.0f, (bool success) => {
					// handle success or failure
				});
				
				break;
			}
		case 80:
			{
				
				Social.ReportProgress ("CgkIxLn4os8TEAIQBQ", 100.0f, (bool success) => {
					// handle success or failure
				});
				
				break;
			}
		case 200:
			{
				
				Social.ReportProgress ("CgkIxLn4os8TEAIQBg", 100.0f, (bool success) => {
					// handle success or failure
				});
				
				break;
			}

		case 250:
			{
				Social.ReportProgress ("CgkIxLn4os8TEAIQBw", 100.0f, (bool success) => {
					// handle success or failure
				});

				break;
			}
		default:
			{
				break;
			}
		}


	}

	void TriggerAchievementConsecutive()
	{
		if (Controller.feed >= 100) {
			PlayerPrefs.SetInt ("Consecutive Wins", PlayerPrefs.GetInt ("Consecutive Wins") + 1);

			if (PlayerPrefs.GetInt ("Consecutive Wins") == 3) {
				Social.ReportProgress ("CgkIxLn4os8TEAIQCg", 100.0f, (bool success) => {

				});
			}
		} 
		else {
			PlayerPrefs.SetInt ("Consecutive Wins", 0);

		}

	}
}
