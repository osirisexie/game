using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorOneInit : Initializer
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameConfig.TutorOneMode ();
		base.OnSceneLoaded (scene, mode);
	}
}

