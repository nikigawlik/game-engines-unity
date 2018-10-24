using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public PlayerController player;

	private static GameController instance;
	public static GameController Instance {
		get {
			return instance;
		}
	}

	private void Start() {
		if(GameController.instance != null) {
			GameObject.Destroy(gameObject);
		} else {
			GameController.instance = this;
		}
	}
}
