using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer: MonoBehaviour
{

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameObject.Find ("Player").AddComponent<PlayerController> ();
		GameObject.Find ("Parents").AddComponent<ParentsController> ();
		GameObject.Find ("Camera").AddComponent<PlayerCamera> ();
	}
}

