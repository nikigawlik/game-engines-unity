using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerController : MonoBehaviour {
	[SerializeField] private float speed;
	private Rigidbody2D rb;
	private Animator anim;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	private void FixedUpdate() {
		PlayerController player = GameController.Instance.player;
		if(player != null) {
			rb.velocity = (player.transform.position - transform.position).normalized;
		}
	}

	private void Update() {
		anim.SetFloat("lookX", rb.velocity.x);
		anim.SetFloat("speed", rb.velocity.magnitude);
	}
}
