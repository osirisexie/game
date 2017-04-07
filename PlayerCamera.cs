using UnityEngine;
using System;

public class PlayerCamera: MonoBehaviour
{
	public GameObject playerObject;
	private PlayerProfile player;

	void Start()
	{
		player = playerObject.GetComponent<PlayerProfile> ();	
	}

	void LateUpdate()
	{
//		Debug.Log (player.status);
	}

}

