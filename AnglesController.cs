using System;
using UnityEngine;
using System.Linq;


public class AnglesController : CollectionsController
{
	void Awake()
	{
		float index = 0;
		foreach (Transform child in transform) {
			child.gameObject.name = "Angle";
			child.gameObject.AddComponent<AngleController> ();
			index++; 
		}
	}
}

