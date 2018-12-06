using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour {
	public Sprite[] sprites;
	private int spriteIndex;
	public int SpriteIndex {
		get {
			return spriteIndex;
		}
		set {
			spriteIndex = Mathf.Clamp(value, 0, sprites.Length - 1);
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			if(sr) sr.sprite = sprites[spriteIndex];
		}
	}

	[EasyButtons.Button]
	public void Test() {
		SpriteIndex++;
	}
}
