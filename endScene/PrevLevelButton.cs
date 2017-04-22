using System;
using UnityEngine;
using UnityEngine.UI;

public class PrevLevelButton: MonoBehaviour
{

	void Start()
	{
		if (SharedData.currentLevel == 0) {
			transform.Find ("Text").GetComponent<Text> ().text = "It's the first level";
		} else {
			Button btn = GetComponent<Button>();
			btn.onClick.AddListener(Next);	
		}

	}

	void Next()
	{
		SharedData.currentLevel--;
		Application.LoadLevel (GameConfig.levels[SharedData.currentLevel]);

	}

}



