using System;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class ParentController: MonoBehaviour
{
	public float scale;
	public bool isParent = false;

	private GameObject gravity;
	private Canvas canvas;

	static System.Random r = new System.Random ();

	void Awake()
	{
		scale = (float)(0.5f + ParentController.r.NextDouble ());
		Vector3 position = transform.position;
		position.z = 0;
		transform.position = position;
		transform.localScale = new Vector3 (scale, scale, 1);
		createFans();
		createCollider ();
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

	private void createFans()
	{
		int fanNum = (int)(3 + (scale - 0.5f) * 5 / 1);
		float angle = Mathf.PI * 2 / fanNum;
		for (int i = 0; i < fanNum; i++) {
			GameObject newFan = new GameObject ();
			newFan.transform.position = transform.position + new Vector3 (3 * scale * Mathf.Cos(angle * i), 3 * scale * Mathf.Sin(angle * i), 0);
			newFan.name = "Fan-" + i;
			newFan.AddComponent<FanController> ();
			newFan.transform.parent = transform;
			SpriteRenderer spriteRender = newFan.AddComponent <SpriteRenderer> ();
			spriteRender.sprite = Resources.Load<Sprite> ("Images/Fan");
		}

	}

	private void createCollider()
	{
		GameObject collider = new GameObject ();
		collider.transform.parent = transform;
		collider.transform.position = transform.position;
		collider.name = "CollisionDummy";
		SphereCollider sphereCollider = collider.AddComponent<SphereCollider> ();
		sphereCollider.radius = 10 * scale;

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

	void Enter()
	{
		isParent = true;
	}

	void Left()
	{
		isParent = false;
	}
}

