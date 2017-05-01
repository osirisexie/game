using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

public class ParentController: MonoBehaviour
{
	public float scale;
	public float orbit;
	public float minDistance;
	public bool isParent = false;

	private GameObject gravity;
	private GameObject passion;
	private float passionIndex = 0;
	private Vector3 baseScale;
	private Canvas canvas;
	private List<GameObject> Fans = new List<GameObject> ();
	static Color green = new Color (176f / 225f, 1f, 176f / 225f, 22f / 225f);
	static Color blue = new Color (189f / 225f, 235f / 225f, 1f, 22f / 225f);


	static float escapeScale = 3f;

	void Awake()
	{
		
	

		scale = (float)(Utility.r.NextDouble ()*(GameConfig.parentScaler[1] - GameConfig.parentScaler[0]) + GameConfig.parentScaler[0]);
		orbit = GameConfig.orbitBase * scale;
		minDistance = GameConfig.minDistanceBase * scale;
		baseScale = new Vector3 (escapeScale, escapeScale, 0);
		Vector3 position = transform.position;
		position.z = 0;
		transform.position = position;
		transform.localScale = new Vector3 (scale, scale, 1);

		if (name != "GameTarget") {
			Utility.ChangeSprite (gameObject, GameConfig.celes [SharedData.parentImg]);
			SharedData.parentImg++;
			SharedData.parentImg %= GameConfig.celes.Length;
			createFans ();
		} else {
			Utility.ChangeSprite (gameObject, GameConfig.targetImg);
		}
		Utility.CreateCollider (transform, 10*scale);
		createGravity ();
		createPassion ();
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
		if (isParent) {
			passionIndex = Mathf.Min(GameConfig.deadSpeed+passionIndex, 1f);
			if (passionIndex == 1 && !SharedData.gameOver) {
				GameObject.Find ("GameUI").SendMessage ("Complete","fail");
				SharedData.gameOver = true;
			}
			passion.transform.localScale = baseScale * passionIndex;

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
		int num = GameConfig.fans.Length;
		int ranFan = Utility.r.Next (num);
		for (int i = 0; i < fanNum; i++) {
			GameObject newFan = new GameObject ();
			newFan.transform.position = transform.position + new Vector3 (3 * scale * Mathf.Cos(angle * i), 3 * scale * Mathf.Sin(angle * i), 0);
			newFan.name = "Fan-" + i;
			newFan.AddComponent<FanController> ();
			newFan.transform.parent = transform;
			Utility.ChangeSprite (newFan, GameConfig.fans [ranFan]);
			Fans.Add (newFan);
		}

	}

	private void createGravity()
	{
		gravity = new GameObject ("gravity");
		gravity.transform.parent = transform;
		gravity.transform.localPosition = Vector3.zero;
		gravity.transform.localScale = baseScale;
		gravity.AddComponent<SpriteRenderer> ();
		SpriteRenderer render = gravity.GetComponent<SpriteRenderer> ();
		render.sprite = Resources.Load<Sprite> ("Images/gravity");
		render.color = green;
		render.sortingOrder = -3;
		gravity.SetActive (false);
	}

	private void createPassion()
	{
		passion = new GameObject ("passion");
		passion.transform.parent = transform;
		passion.transform.localScale = baseScale * passionIndex;
		passion.transform.position = transform.position;
		passion.AddComponent<SpriteRenderer> ();
		SpriteRenderer render = passion.GetComponent<SpriteRenderer> ();
		render.sprite = Resources.Load<Sprite> ("Images/gravity");
		render.color = new Color(1f, 0f, 0f, 0.3f);
		render.sortingOrder = -2;
	}

	void Enter()
	{
		isParent = true;
		gravity.GetComponent<SpriteRenderer> ().color = blue;
		foreach (GameObject fan in Fans) {
			fan.SendMessage ("Enter");
		}
	}

	void Left()
	{
		isParent = false;
		gravity.GetComponent<SpriteRenderer> ().color = green;
		passionIndex = 0;
		passion.transform.localScale = baseScale * passionIndex;
		foreach (GameObject fan in Fans) {
			fan.SendMessage ("Left");
		}

	}
}

