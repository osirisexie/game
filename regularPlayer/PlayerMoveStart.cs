using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerMoveStart : IPlayerMove
{
	protected PlayerData player;
	protected bool start = false;

	public PlayerMoveStart(PlayerData gamePlayer)
	{
		player = gamePlayer;
	}

	public void move()
	{
		
	
	}

	public bool checkIfNextMove ()
	{
		if(start) GameObject.Find("Camera").SendMessage ("PrepareMoving");
		return player.prepared;
	}

	public void prepareNextMove()
	{
		GameObject.Find("Camera").SendMessage ("StartMoving");
		player.status = "line";
	}
	public virtual void keyDown ()
	{
		this.start = true;
	}
	public void noKey ()
	{
		
	}
}
