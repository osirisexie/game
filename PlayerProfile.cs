using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class PlayerProfile : MonoBehaviour
{
	public string status;
	public Stopwatch stopwatch = new Stopwatch();

	public float speed;
	public float speedAddition;
	public float speedMinus;
	public float escapeSpeed;
	public float minSpeed;
	public float maxSpeed;

	public float distanceBase;
	public float energy;
	public float orbitBase;
	public float minDistanceBase;
	public float minDistance;
	public float orbit;

	public float angle;
	public float angleDiff;
	public float angleAddition;
	public float enterAngle;

	public bool clockWise;

	public bool prepared;

	public List<GameObject> parents;
	public IEnumerator particleCoroutine;
	public Vector3 direction;
	public GameObject parent;
	public Transform particle;
	public ParticleSystem particleSystem;
	public GameObject energyBar;
	public GameObject target;

	void Awake(){
		status = "start";
		prepared = false;
		speed = GameConfig.speed;
		speedAddition = GameConfig.speedAddition;
		speedMinus = GameConfig.speedMinus;
		escapeSpeed = GameConfig.escapeSpeed;
		minSpeed = GameConfig.minSpeed;
		maxSpeed = GameConfig.maxSpeed;
		energy = GameConfig.energy;
		distanceBase = GameConfig.distanceBase;
		orbitBase = GameConfig.orbitBase;
		minDistanceBase = GameConfig.minDistanceBase;
		angle = 0;
		angleDiff = 0;
		clockWise = false;
		target = GameObject.Find ("GameTarget");
		energyBar = GameObject.Find ("EnergyBar");
	}

	void Start(){
		angleAddition = distanceBase * speed / minDistance;
		direction = new Vector3 (-distanceBase, 0, 0);
	}
}

