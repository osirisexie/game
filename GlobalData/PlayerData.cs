using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class PlayerData
{

	private static PlayerData _instance;

	public static PlayerData Instance()
	{
		if (_instance == null)
		{
			_instance = new PlayerData();
		}

		return _instance;
	}

	public static void ClearInstace()
	{
		_instance = null;
	}


	public Stopwatch stopwatch = new Stopwatch();

	public string status;
	public float speed;
	public float energy;
	public int angles;

	//rotation shared params
	public float angle;
	public float angleDiff;
	public float angleAddition;
	public float enterAngle;
	public bool clockWise;
	public bool prepared;

	//share components for easy call
	public List<ParentController> parents;
	public Vector3 direction;
	public ParentController parent;
	public Transform particle;
	public ParticleSystem particleSystem;
	public GameObject energyBar;
	public GameObject target;

	//UnityObject
	public Transform transform;
	public GameObject gameObject;


	protected PlayerData ()
	{
		gameObject = GameObject.Find ("Player");
		transform = gameObject.transform;

		status = "start";
		prepared = false;
		speed = GameConfig.speed;
		energy = GameConfig.energy;
		angles = 0;

		angle = 0;
		angleDiff = 0;
		clockWise = false;
		target = GameObject.Find ("GameTarget");
		energyBar = GameObject.Find ("EnergyBar");
		particle = transform.Find ("PlayerParicle");
		particleSystem = particle.Find ("PlayerParticleSystem").GetComponent<ParticleSystem>();
		direction = new Vector3 (-GameConfig.distanceBase, 0, 0);
	}
}

