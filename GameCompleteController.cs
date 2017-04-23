﻿using System;
using UnityEngine;
using System.Linq;

public class GameCompleteController : MonoBehaviour
{
	void Awake(){
		foreach (Transform child in transform) {
			child.gameObject.SetActive (false);
			if (child.name == "Complete") {
				child.gameObject.AddComponent<CompletePanelController> ();
			}
		}
	}

	void Complete(string state){
		try{
			GameObject success = transform.Find ("Complete").gameObject;
			success.SetActive (true);
			success.SendMessage("ChangeWord",state);

		}catch{
		}
	}

}

