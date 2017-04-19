using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


public class PlayerProfile : MonoBehaviour
{
	public Stopwatch stopwatch = new Stopwatch();

	public string status;
	public float speed;
	public float energy;

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

	void Awake(){
		status = "start";
		prepared = false;
		speed = GameConfig.speed;
		energy = GameConfig.energy;

		angle = 0;
		angleDiff = 0;
		clockWise = false;
		target = GameObject.Find ("GameTarget");
		energyBar = GameObject.Find ("EnergyBar");
		particle = transform.Find ("PlayerParicle");
		particleSystem = particle.Find ("PlayerParticleSystem").GetComponent<ParticleSystem>();
		direction = new Vector3 (-GameConfig.distanceBase, 0, 0);

	}

	void Start(){
	}
}

