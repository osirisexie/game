using System;
using UnityEngine;

public class AngleController: CollectionController
{
	void Awake()
	{
		Utility.AddCollider (gameObject, 0.3f);
	}

	void Collected()
	{
		gameObject.SetActive (false);
	}
}

