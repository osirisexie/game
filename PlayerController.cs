using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	
	IPlayerMove moveController;
	PlayerProfile player;
	MicrophoneInput microphone;

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ().enabled = false;

		player = GetComponent<PlayerProfile> ();
		microphone = new MicrophoneInput (player);
		PlayerMoveLine.ClearInstace ();
		PlayerMoveCaptured.ClearInstace ();
		PlayerMoveEscape.ClearInstace ();
		PlayerMoveRotate.ClearInstace ();
//		moveController = PlayerMoveLine.Instance (player);
		moveController = new PlayerMoveStart(player);
	}


	void Start()
	{
		player.stopwatch.Start ();
		//		microphone.StartMicListener ();
	}

	void Update()
	{	
		
//		if (microphone.AnalyzeSound () > -30 || Input.GetKey (KeyCode.UpArrow)) {
//			moveController.keyDown ();
//		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			moveController.keyDown ();
		}
		else {
			moveController.noKey ();
		}
		moveController.move ();
		if (moveController.checkIfNextMove ()) 
		{
			moveController =  moveController.prepareNextMove ();
		}
	}

	private string getCurrentSpeedPercent(){
		if (player.parent != null) {
			return ((int)(player.speed * 100 / player.escapeSpeed)).ToString () + "%";
		} else {
			return "You are free now!";
		}
	}


}

