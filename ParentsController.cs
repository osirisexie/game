using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;


public class ParentsController : MonoBehaviour
{
	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		foreach (Transform child in transform) {
			child.gameObject.AddComponent<ParentController> ();
		}
	}

	
	void Start() {

	}

}
