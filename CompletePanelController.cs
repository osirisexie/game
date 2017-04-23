using System;
using UnityEngine;
using UnityEngine.UI;


public class CompletePanelController : MonoBehaviour
{
	private DataKeeper data;
	private string words;

	void ChangeWord(string state){
		if (state == "success") {
			data = GameObject.Find ("DataKeeper").GetComponent<DataKeeper> ();
			words = "You find your soulmate in " + data.time + " seconds!!";
		} else {
			words = "Game Over !! Try again!";
		}
		transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = words;
		Button btn1 = transform.Find("Retry").gameObject.GetComponent<Button>();
		btn1.onClick.AddListener(replay);

		if (SharedData.currentLevel < GameConfig.levels.Length - 1) {
			Button btn2 = transform.Find ("Next").GetComponent<Button> ();
			btn2.onClick.AddListener (Next);	
		} else {
			transform.Find ("Next").GetComponent<Button> ().transform.Find("Text").GetComponent<Text>().text = "Last";
		}

		if (SharedData.currentLevel != 0) {
			Button btn3 = transform.Find ("Prev").GetComponent<Button> ();
			btn3.onClick.AddListener (Prev);	
		} else {
			transform.Find ("Prev").GetComponent<Button> ().transform.Find("Text").GetComponent<Text>().text = "First";
		}
//		transform.Find("Record").gameObject.GetComponent<UnityEngine.UI.Text>().text = "You beats "+(int)(record*100)+"% players!!";
	}

	void replay()
	{
		Application.LoadLevel (GameConfig.levels[SharedData.currentLevel]);
	}

	void Next()
	{
		SharedData.currentLevel++;
		Application.LoadLevel (GameConfig.levels[SharedData.currentLevel]);

	}

	void Prev()
	{
		SharedData.currentLevel--;
		Application.LoadLevel (GameConfig.levels[SharedData.currentLevel]);

	}
}

