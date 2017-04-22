using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MongoDB.Bson;
using MongoDB.Driver;



public class GameOver : MonoBehaviour
{

	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(replay);
	}

	void replay()
	{
		Application.LoadLevel (GameConfig.levels[SharedData.currentLevel]);
	}

	void OnApplicationQuit()
	{
	}
}

