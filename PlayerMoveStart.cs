using System;
using UnityEngine;

public class PlayerMoveStart : IPlayerMove
{
	private PlayerProfile player;
	private bool start = false;
	private Camera worldCam; 
	private float ratio;
	private float minus;
	private float originalSize;
	private float current = 1;

	public PlayerMoveStart(PlayerProfile gamePlayer)
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

	public IPlayerMove prepareNextMove()
	{
		GameObject.Find("Camera").SendMessage ("StartMoving");
		player.status = "line";
		return PlayerMoveLine.Instance(player);
	}
	public void keyDown ()
	{
		this.start = true;
	}
	public void noKey ()
	{
		
	}
}
