using System;


public interface IPlayerMove
{
	void move();
	bool checkIfNextMove ();
	IPlayerMove prepareNextMove ();
	void keyDown ();
	void noKey ();
}

