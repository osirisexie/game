using System;
using System.Linq;
using System.Collections.Generic;

public static class GameConfig
{
	public static string[] fans = new string[]{"Fan-love","Fan-happy","Fan-confused","Fan-angry"}; 
	public static string[] celes = new string[] {"Celebrity-aj","Celebrity-b"
		,"Celebrity-bbt","Celebrity-bl","Celebrity-h","Celebrity-jc","Celebrity-kw","Celebrity-mz","Celebrity-ts","Celebrity-x"};


	//difficulty
	//Player
	public static float speed = 1f;
	public static float speedAddition = 0.02f;
	public static float speedMinus = 0.02f;
	public static float escapeSpeed = 5f;
	public static float minSpeed = 1f;
	public static float maxSpeed = 5f;
	public static float energy = 1.5f;


	//Parent
	public static float distanceBase = .05f;
	public static float orbitBase = 4f;
	public static float minDistanceBase = 6f;

	public static float deadSpeed = 0.003f;
	public static float energyConsume = 0.005f;
	public static float energyRecovery = 0.008f;

	//Camera
	public static float camSize = 10;
//	public static int wait = 200;
	public static float reziseSpeed = 1f/80f;
}

