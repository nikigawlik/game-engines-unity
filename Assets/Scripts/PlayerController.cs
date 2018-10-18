using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float speed = 2f;
	public float speedModifier = 1f;

	private Animator animator;
	private Rigidbody2D rigidbody2d;

	private void Start() {
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = input.normalized;

		if(input.magnitude > 0.01) {
			animator.SetFloat("moveX", input.x);
			animator.SetFloat("moveY", input.y);
		}
		
		rigidbody2d.velocity = input * speed * speedModifier;
	}

	private void OnTriggerStay2D(Collider2D other) {
		Swamp swamp = other.GetComponent<Swamp>();
		if(swamp != null) {
			speedModifier = swamp.Slowdown;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		speedModifier = 1f;
	}
}
