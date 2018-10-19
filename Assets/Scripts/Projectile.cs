using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
	// the projectile will ignore this object
	[HideInInspector] public GameObject shooter;

	private Animator anim;
	private Rigidbody2D rb;

	private void Awake() {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	public void EndOfImpactAnimation() {
		// we don't destroy bec. we are in an object pool
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		// don't collide with the shooter
		if(other.gameObject == shooter) {
			return;
		}
		// hit something
		rb.velocity = Vector2.zero;

		if(anim == null) {
			gameObject.SetActive(false);
		} else {
			anim.SetTrigger("impact");
		}
	}
}
