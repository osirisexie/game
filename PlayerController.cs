using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	
	IPlayerMove moveController;
	PlayerProfile player;
	MicrophoneInput microphone;

	void Awake(){
		GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ().enabled = false;
		player = gameObject.AddComponent<PlayerProfile> ();
		microphone = new MicrophoneInput (player);
		PlayerMoveLine.ClearInstace ();
		PlayerMoveCaptured.ClearInstace ();
		PlayerMoveEscape.ClearInstace ();
		PlayerMoveRotate.ClearInstace ();
		if(GameConfig.mic) moveController = PlayerMoveLine.Instance (player);
		moveController = new PlayerMoveStart(player);
	}


	void Start()
	{
		player.stopwatch.Start ();
		if(GameConfig.mic) microphone.StartMicListener ();
	}

	void Update()
	{	
		if (GameConfig.mic) {
			if (microphone.AnalyzeSound () > -30 || Input.GetKey (KeyCode.UpArrow)) {
				moveController.keyDown ();
			}else {
				moveController.noKey ();
			}
		} else {
			if (Input.GetKey (KeyCode.UpArrow)) {
				moveController.keyDown ();
			}
			else {
				moveController.noKey ();
			}
		}

		moveController.move ();
		if (moveController.checkIfNextMove ()) 
		{
			moveController =  moveController.prepareNextMove ();
		}
	}

	private string getCurrentSpeedPercent(){
		if (player.parent != null) {
			return ((int)(player.speed * 100 / GameConfig.escapeSpeed)).ToString () + "%";
		} else {
			return "You are free now!";
		}
	}


}

