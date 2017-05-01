using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public delegate void DbCallback(float percent);

public class CompletePanelController : MonoBehaviour
{
	private string line1;
	private string line2;
	private bool complete = false;

	void ChangeWord(string state){
		complete = true;

		if (state == "success") {
			line1 = "You find your soulmate in " + SharedData.time + " seconds!!";
			line2 = "Retriveing records from server...";
			Mongo.second = SharedData.time;
			Mongo.level = SharedData.currentLevel;
			Mongo.callback = new DbCallback (changeData);
			SharedData.cauculated = false;
			Thread DbHanler = new Thread (new ThreadStart (Mongo.Add));
			DbHanler.Start ();
		} else {
			line1 = "Game Over !!";
			line2 = "Try Again!!";
		}

		transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = line1;
		transform.Find("Record").gameObject.GetComponent<UnityEngine.UI.Text>().text = line2;
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
	
	}

	public void changeData(float data){
		SharedData.percent = (int)(data * 100);
		SharedData.cauculated = true;
	}

	void Update(){
		if (SharedData.cauculated && complete) {
			transform.Find("Record").gameObject.GetComponent<UnityEngine.UI.Text>().text = "You beats "+SharedData.percent+"% players!!";
			SharedData.cauculated = false;
			complete = false;
		}
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

