using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ParentsController : MonoBehaviour
{

	void Awake(){
		int num = GameConfig.celes.Length;
		SharedData.parentImg = Utility.r.Next (num);
		foreach (Transform child in transform) {
			child.gameObject.AddComponent<ParentController> ();
		}
	}
	
	void Start() {

	}

}
