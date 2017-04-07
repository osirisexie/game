using System;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class ParentController: MonoBehaviour
{
	private GameObject gravity;

	void Start()
	{
		string pattern = "^Fans";
		Regex rgx = new Regex(pattern);
		foreach (Transform child in transform) {
			if (rgx.IsMatch (child.name)) {
				child.gameObject.AddComponent<FanController> ();
			};
		}
//		gravity = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//		gravity.name = "gravity";
//		gravity.transform.localScale = new Vector3 (10, 10, 0);
//		gravity.transform.parent = transform;
//		gravity.transform.position = transform.position;
//		MeshRenderer mesh = gravity.GetComponent<MeshRenderer> ();
//		mesh.material = Resources.Load ("Materials/GravityMaterial") as Material;
//			
//		gravity.SetActive (false);
	}

	void Update(){
		if (checkNearByPlayer ()) {
//			gravity.SetActive (true);
		} else {
//			gravity.SetActive (false);
		}
	}

	public bool checkNearByPlayer(){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 10f);
		List<GameObject> hits = hitColliders.Select (hitCollider => hitCollider.gameObject.transform.parent.gameObject).ToList();
		foreach (GameObject hit in hits) {
			if (hit.name == "Player") {
				return true;
			}
		}
		return false;
	}
}

