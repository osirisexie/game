using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerProfile : MonoBehaviour
{
	public string status = "line";

	public float speed = 1f;
	public float speedAddition = 0.02f;
	public float speedMinus = 0.02f;
	public float escapeSpeed = 5f;
	public float minSpeed = 1f;
	public float maxSpeed = 5f;

	public float distanceBase = .05f;
	public float energy = 1.5f;
	public float orbitBase = 4f;
	public float minDistanceBase = 6f;
	public float minDistance;
	public float orbit;

	public float angle = 0f;
	public float angleDiff = 0;
	public float angleAddition;
	public float enterAngle;

	public bool clockWise = false;

	public float cameraMovingSpeed = 0.05f;

	public List<GameObject> parents;
	public IEnumerator particleCoroutine;
	public Vector3 direction;
	public GameObject parent;
	public Transform particle;
	public ParticleSystem particleSystem;
	public GameObject energyBar;
	public Camera cam;
	public GameObject target;

	void Start(){
		angleAddition = distanceBase * speed / minDistance;
		direction = new Vector3 (-distanceBase, 0, 0);
	}
}

