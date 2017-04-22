using System;
using UnityEngine;
using System.Collections.Generic;

public class TutorController : MonoBehaviour
{
	private Dictionary<string, string> stateDic = new Dictionary<string,string>();

	void Awake(){
		stateDic.Add ("rotate", "ShowWhenRotate");
	}

	void Enter(string state){
		if (stateDic.ContainsKey (state)) {
			try  
			{  
				transform.Find (stateDic [state]).gameObject.SetActive (true);
			}
			catch{
				
			}
		}
	}

	void Left(string state){
		if (stateDic.ContainsKey (state)) {
			try
			{
				transform.Find (stateDic [state]).gameObject.SetActive (false);
			}
			catch
			{
			}
		}
	}
}

