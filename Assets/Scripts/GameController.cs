using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public PlayerController player;

	[Header("UI")]
	public GameObject gameOverUI;
	public Image waveFillImage;

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

		Time.timeScale = 1f;
		
		Health health = player.GetComponent<Health>();
		health.OnHealthUpdated += CheckForGameOver;
	}

	private void CheckForGameOver(int newHealth, int oldHealth) {
		if(newHealth <= 0) {
			// game over
			gameOverUI.SetActive(true);
			Time.timeScale = 0f;
		}
	}

	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void EndGame() {
		Application.Quit();
	}
}
