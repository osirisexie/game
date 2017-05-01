using System;
using System.Linq;
using System.Collections.Generic;

public static class GameConfig
{
	/*
	 * 
	 * Globals
	 * 
	 */
	public static string[] fans = new string[]{"Fan-bl","Fan-glry","Fan-xs"}; 
	public static string[] celes = new string[] {"Celebrity-aj","Celebrity-b","Celebrity-bb"
		,"Celebrity-bbt","Celebrity-bl","Celebrity-ff8","Celebrity-got","Celebrity-h","Celebrity-jc","Celebrity-kw","Celebrity-mz","Celebrity-lost","Celebrity-s","Celebrity-ts","Celebrity-x"};
	public static string[] levels = new string[]{"LevelOne","LevelTwo","LevelThree","LevelEasy","LevelMedium","LevelFinal"};
	public static string playerImg = "player-new";
	public static string targetImg = "soulmate-new";

	//Enable mic
	public static bool mic = false;

	/*
	 * 
	 * Difficulty
	 * 
	 */
	//Player
	public static float speed = 1f;
	public static float speedAddition = 0.02f;
	public static float speedMinus = 0.02f;
	public static float escapeSpeed = 5f;
	public static float minSpeed = 1f;
	public static float maxSpeed = 5f;
	public static float energy = 1.5f;
	public static int angels = 3;

	//Parent
	public static double[] parentScaler = new double[]{0.5, 1.5};

	public static float distanceBase = .05f;
	public static float orbitBase = 4f;
	public static float minDistanceBase = 6f;

	public static float deadSpeed = 0.003f;
	public static float energyConsume = 0.005f;
	public static float energyRecovery = 0.008f;

	//Camera
	public static float camSize = 10;
	public static float reziseSpeed = 1f/80f;
	public static float worldHeight = 30;
	public static float worldWidth = 50;

	/*
	 * 
	 * Switching Modes
	 * 
	 */

	public static void TutorOneMode(){
		deadSpeed = 0.000001f;
		energyConsume = 0.000f;
		parentScaler = new double[]{ 1.5, 1.5 };
		angels = 0;
		worldHeight = 30;
		worldWidth = 45;
	}

	public static void TutorTwoMode(){
		deadSpeed = 0.000001f;
		energyConsume = 0.003f;
		parentScaler = new double[]{ 1.5, 1.5 };
		angels = 0;
		worldHeight = 30;
		worldWidth = 45;
	}

	public static void TutorThreeMode(){
		deadSpeed = 0.002f;
		energyConsume = 0.003f;
		parentScaler = new double[]{ 1.5, 1.5 };
		angels = 0;
		worldHeight = 30;
		worldWidth = 45;
	}

	public static void HardMode(){
		deadSpeed = 0.003f;
		energyConsume = 0.004f;
		parentScaler = new double[]{ 0.5, 1.5 };
		angels = 3;
		worldHeight = 50;
		worldWidth = 90;
	}

	public static void EasyMode(){
		deadSpeed = 0.002f;
		energyConsume = 0.003f;
		parentScaler = new double[]{ 0.7, 1.3 };
		angels = 2;
		worldHeight = 30;
		worldWidth = 40;
	}

	public static void MediumMode(){
		deadSpeed = 0.002f;
		energyConsume = 0.003f;
		parentScaler = new double[]{ 0.7, 1.3 };
		angels = 2;
		worldHeight = 40;
		worldWidth = 55;
	}
}

