using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorTwoInit : Initializer
{
	protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameConfig.TutorTwoMode ();
		base.OnSceneLoaded (scene, mode);
	}
}

