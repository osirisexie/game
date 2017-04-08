using System;
using UnityEngine;

public class FanController: MonoBehaviour
{
	private float angleAddition = 0.02f;
	private float angle;
	private float distance;
	private float jump;

	static System.Random r = new System.Random ();


	void Start()
	{
		Vector3 direction = transform.parent.transform.position - transform.position;
		distance = direction.magnitude;
		angle = Mathf.Atan2 (direction.y, direction.x);
		jump = (float)(r.NextDouble () * 8 + 10f);
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
}

