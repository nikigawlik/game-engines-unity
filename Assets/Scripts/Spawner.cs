using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Spawner : MonoBehaviour {
	public Transform[] spawnpoints;
	public GameObject enemy;
	public float gameStartDelay = 5;
	public float spawnDelay = 10;

	private int waveCounter = 0;
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
				// every 3 waves one more bulldozer spawns
				for (int i = 0; i < 4 + waveCounter / 3; i++) {
					Transform spawnpoint = spawnpoints[i % spawnpoints.Length];
					StartCoroutine(SpawnEnemy(spawnpoint, i / spawnpoints.Length));
				}
			}
		}

		// update UI
		GameController.Instance.waveFillImage.fillAmount = (spawnCountdown / spawnDelay);
		GameController.Instance.waveCounterText.text = waveCounter.ToString();
	}

	private IEnumerator SpawnEnemy(Transform spawnpoint, int offset) {
		yield return new WaitForSeconds(offset);
		GameObject spawned = Instantiate(enemy, spawnpoint.position, Quaternion.identity);
		spawned.transform.SetParent(spawnpoint);
	}
}
