using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Spawner : MonoBehaviour {
	public Transform[] spawnpoints;
	public GameObject enemy;
	public float gameStartDelay = 5;
	public float spawnDelay = 10;

	private float waveCounter = 0;
	private float spawnCountdown;

	[EasyButtons.Button]
	void AutoAssignSpawnpoints() {
		Transform[] children = gameObject.GetComponentsInChildren<Transform>();

		List<Transform> matches = new List<Transform>();

		foreach(Transform t in children) {
			if(t == transform) {
				continue;
			}

			if(Regex.IsMatch(t.gameObject.name, @"(S|s)pawn*")) {
				matches.Add(t);
			}
		}

		spawnpoints = matches.ToArray();
	}
	
	void Start () {
		spawnCountdown = gameStartDelay;
	}

	private void Update() {
		spawnCountdown -= Time.deltaTime;

		if(spawnCountdown <= 0) {
			spawnCountdown = spawnDelay;
			waveCounter++;

			PlayerController player = GameController.Instance.player;
			if (player != null) {
				foreach(Transform t in spawnpoints) {
					GameObject spawned = Instantiate(enemy, t.position, Quaternion.identity);
					spawned.transform.SetParent(t);
				}
			} 
		}

		// update UI
		GameController.Instance.waveFillImage.fillAmount = (spawnCountdown / spawnDelay);
		GameController.Instance.waveCounterText.text = waveCounter.ToString();
	}
}
