using UnityEngine;
using System.Collections;

public class Record : MonoBehaviour {

	public static int record=0;
	public static Record instance = null;   

	void Awake()
	{

		record = PlayerPrefs.GetInt("Player Score");

		

	

}
}