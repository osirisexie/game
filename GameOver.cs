using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MongoDB.Bson;
using MongoDB.Driver;



public class GameOver : MonoBehaviour
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
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(replay);
	}

	void replay()
	{
		Application.LoadLevel ("LevelOne");
	}

	void OnApplicationQuit()
	{
	}
}

