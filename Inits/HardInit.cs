using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardInit : Initializer
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameConfig.HardMode ();
		base.OnSceneLoaded (scene, mode);
	}
}

