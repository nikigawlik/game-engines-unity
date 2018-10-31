using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject enemy;
	public int frequency = 5;
	
	void Start () {
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn() {
		yield return new WaitForSeconds(frequency);
		while (true) {
			PlayerController player = GameController.Instance.player;
			if (player != null) {
				GameObject spawned = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
				spawned.transform.SetParent(gameObject.transform);
				yield return new WaitForSeconds(frequency);
			} else {
				break;
			}
		}
	}
}
