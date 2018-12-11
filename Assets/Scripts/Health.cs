using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public int max = 5;
	private bool isInvincible = false;
	public int startHealth = 0;
	public float invincibleDuration = 0;
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
		current = startHealth > 0 ? startHealth : max;
		sprite = gameObject.GetComponent<SpriteRenderer>();
		isPlayer = gameObject.GetComponent<PlayerController>() != null;
	}

	private void SetHealth (int n) {
		if (OnHealthUpdated != null)
			OnHealthUpdated(n, current);
		current = n;
	}

	public void ApplyDamage (int n) {
		if (isInvincible) return;
		SetHealth(Mathf.Max(current - n, 0));
		if (current <= 0) {
			GameObject.Destroy(gameObject);
		} else {
			if (invincibleDuration > 0) {
				StartCoroutine(MakeInvincible(invincibleDuration));
			}
			if (!isPlayer) {
				StartCoroutine(ShowDamage());
			}
		}
	}

	public void Heal (int n) {
		SetHealth(Mathf.Min(max, current + n));
	}

	public IEnumerator MakeInvincible(float duration) {
		isInvincible = true;
		Color objColor = sprite.color;
		objColor.a = 0.3f;
		sprite.color = objColor;
		yield return new WaitForSeconds(duration);
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
