using System;
using UnityEngine;
using UnityEngine.UI;



public class NextLevelButton: MonoBehaviour
{

	void Start()
	{
		if (SharedData.currentLevel + 1 == GameConfig.levels.Length) {
			transform.Find ("Text").GetComponent<Text> ().text = "It's the last level";
		} else {
			Button btn = GetComponent<Button>();
			btn.onClick.AddListener(Next);	
		}
	
	}

	void Next()
	{
		SharedData.currentLevel++;
		Application.LoadLevel (GameConfig.levels[SharedData.currentLevel]);

	}

}

