using System;
using UnityEngine;


public static class Utility
{
	public static System.Random r = new System.Random ();

	public static void ChangeSprite(GameObject obj, string sprite){
		try{
			SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = Resources.Load<Sprite> ("Images/"+sprite);	
		}catch{
			SpriteRenderer spriteRender = obj.AddComponent <SpriteRenderer> ();

			spriteRender.sprite = Resources.Load<Sprite> ("Images/"+sprite);
		}

	}

	public static void CreateCollider(Transform transform, float radius)
	{
		GameObject collider = new GameObject ();
		collider.transform.parent = transform;
		collider.transform.position = transform.position;
		collider.name = "CollisionDummy";
		SphereCollider sphereCollider = collider.AddComponent<SphereCollider> ();
		sphereCollider.radius = radius;

	}

	public static void AddCollider(GameObject gameObject, float radius)
	{
		SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider> ();
		sphereCollider.radius = radius;
	}
}
