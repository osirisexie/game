using System;
using UnityEngine;

public class FanController: MonoBehaviour
{
	private float angleAddition = 0.02f;
	private float angle;
	private float distance;

	void Start()
	{
		Vector3 direction = transform.parent.transform.position - transform.position;
		distance = direction.magnitude;
		angle = Mathf.Atan2 (direction.y, direction.x);
	}

	void Update()
	{
		angle += angleAddition;
		transform.position = new Vector3 (distance * Mathf.Cos (angle), distance * Mathf.Sin (angle), 0) + transform.parent.transform.position;
	}
}

