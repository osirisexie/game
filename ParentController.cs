using System;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class ParentController: MonoBehaviour
{
	private GameObject gravity;
	private Canvas canvas;
	public float scale;
	static System.Random r = new System.Random ();

	void Awake()
	{
		scale = (float)(0.5f + ParentController.r.NextDouble ());
		transform.localScale = new Vector3 (scale, scale, 1);
		GameObject collider = transform.Find ("Sphere").gameObject;
		SphereCollider sphereCollider = collider.GetComponent<SphereCollider> ();
		sphereCollider.radius *= scale;

		string pattern = "^Fans";
		Regex rgx = new Regex(pattern);
		foreach (Transform child in transform) {
			if (rgx.IsMatch (child.name)) {
				child.gameObject.AddComponent<FanController> ();
			};
		}

		createGravity ();
	}

	void Start()
	{

	
	}
		

	void Update(){
		if (checkNearByPlayer ()) {
			gravity.SetActive (true);
		} else {
			gravity.SetActive (false);
		}
	}

	public bool checkNearByPlayer(){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 8f);
		List<GameObject> hits = hitColliders.Select (hitCollider => hitCollider.gameObject.transform.parent.gameObject).ToList();
		foreach (GameObject hit in hits) {
			if (hit.name == "Player") {
				return true;
			}
		}
		return false;
	}

	private void createGravity()
	{
		gravity = new GameObject ("gravity");
		gravity.transform.localScale = new Vector3 (6, 6, 0) * scale;
		gravity.transform.parent = transform;
		gravity.transform.position = transform.position;
		gravity.AddComponent<SpriteRenderer> ();
		SpriteRenderer render = gravity.GetComponent<SpriteRenderer> ();
		render.sprite = Resources.Load<Sprite> ("Images/gravity");
		render.color = new Color(1f, 1f, 1f, 0.3f);
		render.sortingOrder = -1;
		gravity.SetActive (false);
	}
}

