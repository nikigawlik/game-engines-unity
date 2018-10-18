using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float force = 2f;

	private Animator animator;
	private Rigidbody2D rigidbody2d;

	private void Start() {
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = input.normalized;
		int d = (int)(Mathf.Round(Mathf.Atan2(input.y, input.x) / (2f * Mathf.PI)) * 4) % 4;

		if(input.magnitude > 0.01) {
			animator.SetFloat("moveX", input.x);
			animator.SetFloat("moveY", input.y);

			rigidbody2d.AddForce(input * force);
		}
	}
}
