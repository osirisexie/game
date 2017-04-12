using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FanController: MonoBehaviour
{
	private float angleAddition;
	private float angle;
	private float distance;
	private float jump;
	private GameObject particle;

	static System.Random r = new System.Random ();

	void Awake()
	{
		particle = Instantiate (GameObject.Find ("HeartParticle").gameObject, transform);
		particle.transform.localPosition = Vector3.zero;
		particle.transform.localScale = Vector3.one;
		particle.SetActive (false);
	}
		
	void Start()
	{
		Vector3 direction = transform.parent.transform.position - transform.position;
		distance = direction.magnitude;
		angle = Mathf.Atan2 (direction.y, direction.x);
		jump = (float)(r.NextDouble () * 5 + 5f);
		angleAddition = (float)(r.NextDouble () * 0.01f + 0.02f);
	}

	void Update()
	{
		angle += angleAddition;
		float adjuctedDistance;
		if (transform.parent.GetComponent<ParentController> ().isParent) {
			adjuctedDistance = distance * (1 + (Mathf.Sin (angle * jump) * 0.15f + 0.15f));
		} else {
			adjuctedDistance = distance;
		}
//		adjuctedDistance = distance;

		transform.position = new Vector3 (adjuctedDistance * Mathf.Cos (angle), adjuctedDistance * Mathf.Sin (angle), 0) + transform.parent.transform.position;
	}

	void Enter(){
		particle.SetActive (true);
	}

	void Left(){
		particle.SetActive (false);
	}
}

