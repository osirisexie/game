using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MediumInit : Initializer
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameConfig.MediumMode ();
		base.OnSceneLoaded (scene, mode);
	}
}

