using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class Spawner : MonoBehaviour {
	public Tilemap tilemap;
	public Transform[] spawnpoints;
	public GameObject enemy;
	public GameObject tree;
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
			SpawnTree();

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

	private void SpawnTree() {
		BoundsInt bounds = tilemap.cellBounds;
		TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
		List<Vector2> possiblePositions = new List<Vector2>();
		for (int x = 0; x < bounds.size.x; x++) {
			for (int y = 0; y < bounds.size.y; y++) {
				TileBase tile = allTiles[x + y * bounds.size.x];
				// tilemap_0 = grass
				if (tile != null && tile.name == "tilemap_0") {
					Vector2 point = new Vector2(x, y) + (Vector2)(Vector3)bounds.position + new Vector2(0.5f, 0.5f);
					Collider2D occupied = Physics2D.OverlapPoint(point);
					if (occupied == null) {
						possiblePositions.Add(point);
					}
				}
			}
		}
		Vector2 position = possiblePositions[Random.Range(0, possiblePositions.Count)];
		GameObject spawned = Instantiate(tree, position, Quaternion.identity);
		spawned.transform.SetParent(gameObject.transform);
	}

	private IEnumerator SpawnEnemy(Transform spawnpoint, int offset) {
		yield return new WaitForSeconds(offset);
		GameObject spawned = Instantiate(enemy, spawnpoint.position, Quaternion.identity);
		spawned.transform.SetParent(spawnpoint);
	}
}
