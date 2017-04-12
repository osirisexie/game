using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MongoDB.Bson;
using MongoDB.Driver;



public class CompleteController:MonoBehaviour
{


	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
	}

	void Start()
	{
		int time = GameObject.Find ("DataKeeper").GetComponent<DataKeeper> ().time;
		GameObject.Find("ResultText").GetComponent<UnityEngine.UI.Text>().text = "You find your soulmate in "+time+" seconds!!";
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(replay);
		Mongo.Open ();
		float record = Mongo.Add (time);
		GameObject.Find("Record").GetComponent<UnityEngine.UI.Text>().text = "You beats "+(int)(record*100)+"% players!!";

	}

	void replay()
	{
		Mongo.Close ();
		Application.LoadLevel ("LevelOne");
	}

	void OnApplicationQuit()
	{
		Mongo.Close ();
	}
}


