using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHUD : MonoBehaviour {
	public PlayerController player;
	public GameObject heart;
	private GameObject[] hearts;
	private Health health;

	void Start () {
		health = player.GetComponent<Health>();
		health.OnHealthUpdated += HandleOnHealthUpdated;
		hearts = new GameObject[health.max];
		for (int i = 0; i < hearts.Length; i++) {
			GameObject created = Instantiate(heart);
			created.transform.SetParent(gameObject.transform);
			if (i + 1 > health.Current) {
				created.SetActive(false);
			}
			hearts[i] = created;
		}
	}

	void HandleOnHealthUpdated (int newHealth, int oldHealth) {
		if (newHealth < oldHealth) {
			for (int i = oldHealth - 1; i >= newHealth; i--) {
				hearts[i].SetActive(false);
			}
		} else {
			for (int i = oldHealth; i < newHealth; i++) {
				hearts[i].SetActive(true);
			}
		}
	}
}
