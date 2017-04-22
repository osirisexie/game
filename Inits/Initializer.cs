using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class Initializer: MonoBehaviour
{

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		SharedData.currentLevel = GameConfig.levels.ToList().FindIndex(a=>a == scene.name);
		PlayerMoveLine.ClearInstace ();
		PlayerMoveCaptured.ClearInstace ();
		PlayerMoveEscape.ClearInstace ();
		PlayerMoveRotate.ClearInstace ();
		if (SharedData.currentLevel <= 2) {
			GameObject.Find ("Player").AddComponent<TutorPlayerController> ();
			GameObject.Find ("StaticTutor").AddComponent<TutorController> ();
		} else {
			GameObject.Find ("Player").AddComponent<PlayerController> ();
		}
		GameObject.Find ("Parents").AddComponent<ParentsController> ();
		GameObject.Find ("Camera").AddComponent<PlayerCamera> ();
	}
}

