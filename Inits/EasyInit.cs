using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyInit : Initializer
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameConfig.EasyMode ();
		base.OnSceneLoaded (scene, mode);
	}
}

