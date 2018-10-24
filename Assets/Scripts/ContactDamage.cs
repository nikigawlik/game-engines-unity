using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {
	[SerializeField] private int damage = 1;

	private void OnCollisionEnter2D(Collision2D other) {
		Health health = other.gameObject.GetComponent<Health>();
		if (gameObject.tag != other.gameObject.tag && health) {
			health.ApplyDamage(damage);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Health health = other.gameObject.GetComponent<Health>();
		if (gameObject.tag != other.gameObject.tag && health) {
			health.ApplyDamage(damage);
		}
	}
}
