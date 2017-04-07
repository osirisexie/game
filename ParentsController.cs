using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class ParentsController : MonoBehaviour
{
	void Start() {
		foreach (Transform child in transform) {
			child.gameObject.AddComponent<ParentController> ();
		}
	}

}
