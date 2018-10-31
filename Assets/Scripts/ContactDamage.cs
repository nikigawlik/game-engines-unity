using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {
	[SerializeField] private int damage = 1;
	public bool damageOnce = false;

	private bool hasDamaged = false;

	private void OnCollisionStay2D(Collision2D other) {
        ApplyDamage(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        ApplyDamage(other.gameObject);
	}

    private void ApplyDamage(GameObject other) {
        Health health = other.GetComponent<Health>();
        if (gameObject.tag != other.tag && health && !(damageOnce && hasDamaged)) {
            health.ApplyDamage(damage);
            hasDamaged = true;
        }
    }
}
