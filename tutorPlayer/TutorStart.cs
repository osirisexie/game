using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class TutorStart: PlayerMoveStart
{
	private bool init =false;
	private int current = 1;

	private List<Transform> tutors = new List<Transform> ();

	public TutorStart(PlayerProfile gamePlayer):base(gamePlayer)
	{
		if (GameObject.Find ("Tutor") != null) {
			foreach (Transform child in GameObject.Find("Tutor").transform) {
				child.gameObject.SetActive (false);
				tutors.Add (child);
			}
			tutors [0].gameObject.SetActive (true);
		}
		foreach (Transform child in GameObject.Find("StaticTutor").transform) {
			if (child.name != "ShowWhenStart") {
				child.gameObject.SetActive (false);
			}
		}

	
	}

	public override void keyDown ()
	{
		if (current < tutors.Count ()) {
			if (current == 1) {
				tutors [current].transform.position = player.transform.position + new Vector3 (0, 1, 0);	
			}
			if (current == 2) {
				tutors [current].transform.position = player.target.transform.position + new Vector3 (0, 4, 0);	
			}
			tutors [current - 1].gameObject.SetActive (false);
			tutors [current].gameObject.SetActive (true);
			current++;
		} else if (tutors.Count () >= 1) {
			tutors [current - 1].gameObject.SetActive (false);
			this.start = true;
		} else {
			this.start = true;
		}
	

	}


}

