using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorThreeInit : Initializer
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameConfig.TutorThreeMode ();
		base.OnSceneLoaded (scene, mode);
	}
}

