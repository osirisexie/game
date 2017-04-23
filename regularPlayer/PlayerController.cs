using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	
	IPlayerMove moveController;
	protected PlayerProfile player;
	MicrophoneInput microphone;
	protected Dictionary<string, IPlayerMove> movesDic = new Dictionary<string, IPlayerMove>();


	void Awake(){
		Utility.ChangeSprite (gameObject, GameConfig.playerImg);
		GameObject.FindGameObjectWithTag ("World").GetComponent<Camera> ().enabled = false;
		player = gameObject.AddComponent<PlayerProfile> ();
//		microphone = new MicrophoneInput (player);
//		if(GameConfig.mic) moveController = PlayerMoveLine.Instance (player);
		initMoveDic(player);
		moveController = movesDic["start"];

	}

	protected virtual void initMoveDic(PlayerProfile player){
		movesDic.Add ("start", new PlayerMoveStart(player));
		movesDic.Add ("line", PlayerMoveLine.Instance (player));
		movesDic.Add ("rotate", PlayerMoveRotate.Instance (player));
		movesDic.Add ("captured", PlayerMoveCaptured.Instance (player));
		movesDic.Add ("escape", PlayerMoveEscape.Instance (player));
	}


	void Start()
	{
		player.stopwatch.Start ();
		if(GameConfig.mic) microphone.StartMicListener ();
	}

	void Update()
	{	
		if(player.status == "start"){
			if (Input.GetKeyDown (KeyCode.Space)) {
				moveController.keyDown ();
			} else {
				moveController.noKey ();
			}
		}
//		else if (GameConfig.mic) {
//			if (microphone.AnalyzeSound () > -30 || Input.GetKey (KeyCode.UpArrow)) {
//				moveController.keyDown ();
//			}else {
//				moveController.noKey ();
//			}
//		}
		else {
			if (Input.GetKey (KeyCode.Space)) {
				moveController.keyDown ();
			}
			else {
				moveController.noKey ();
			}
		}

		moveController.move ();
		if (moveController.checkIfNextMove ()) 
		{
			UpdateMove ();
		}
	}

	protected virtual void UpdateMove(){
		moveController.prepareNextMove ();
		moveController =  movesDic[player.status];
	}
	

//f	private string getCurrentSpeedPercent(){
//		if (player.parent != null) {
//			return ((int)(player.speed * 100 / GameConfig.escapeSpeed)).ToString () + "%";
//		} else {
//			return "You are free now!";
//		}
//	}


}

