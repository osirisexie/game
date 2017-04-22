using System;
using UnityEngine;

public class DataKeeper:MonoBehaviour
{
	public double time = 0;

	public int currentLevel = 0;

	void Start()
	{
		DontDestroyOnLoad (gameObject);
	}
}

