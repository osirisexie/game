using System;
using System.Collections.Generic;


public interface IPlayerMove
{
	void move();
	bool checkIfNextMove ();
	void prepareNextMove ();
	void keyDown ();
	void noKey ();
}

