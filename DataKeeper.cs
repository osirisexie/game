using System;
using UnityEngine;

public class DataKeeper:MonoBehaviour
{
	public int time = 0;

	void Start()
	{
		DontDestroyOnLoad (gameObject);
	}
}

