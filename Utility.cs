using System;
using UnityEngine;


public static class Utility
{
	public static void ChangeSprite(GameObject obj, string sprite){
		try{
			SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = Resources.Load<Sprite> ("Images/"+sprite);	
		}catch{
			SpriteRenderer spriteRender = obj.AddComponent <SpriteRenderer> ();

			spriteRender.sprite = Resources.Load<Sprite> ("Images/"+sprite);
		}

	}
}
