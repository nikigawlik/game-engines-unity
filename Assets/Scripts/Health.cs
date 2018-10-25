using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public int max = 5;
	[SerializeField] private bool isInvincible = false;
	[SerializeField] private float inivincibleDuration = 5f; 
	private bool isPlayer;
	private int current;
	private SpriteRenderer sprite;
	
	public delegate void HealthUpdate(int newHealth, int oldHealth);
	public event HealthUpdate OnHealthUpdated;

	public int Current {
		get {
			return current;
		}
	}

	void Start () {
		current = max;
		sprite = gameObject.GetComponent<SpriteRenderer>();
		isPlayer = gameObject.GetComponent<PlayerController>() != null;
	}

	private void SetHealth (int n) {
		if (OnHealthUpdated != null)
			OnHealthUpdated(n, current);
		current = n;
	}

	public void ApplyDamage (int n) {
		if (!isInvincible) {
			SetHealth(Mathf.Max(current - n, 0));
			if (current <= 0) {
				GameObject.Destroy(gameObject);
			} else if (isPlayer) {
				StartCoroutine(MakeInvincible());
			} else {
				StartCoroutine(ShowDamage());
			}
		}
	}

	public void Heal (int n) {
		SetHealth(Mathf.Min(max, current + n));
	}

	private IEnumerator MakeInvincible() {
		isInvincible = true;
		Color objColor = sprite.color;
		objColor.a = 0.3f;
		sprite.color = objColor;
		yield return new WaitForSeconds(inivincibleDuration);
		isInvincible = false;
		objColor.a = 1;
		sprite.color = objColor;
	}

	private IEnumerator ShowDamage() {
		sprite.color = new Color(1, 0.5f, 0.5f);
		yield return new WaitForSeconds(0.15f);
		sprite.color = new Color(1, 1, 1);
	}
}
