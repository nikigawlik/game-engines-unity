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
            Vector3 vectorToPlayer = (player.transform.position - transform.position).normalized;
            rb.velocity = vectorToPlayer * speed;
		
			anim.SetFloat("lookX", vectorToPlayer.x);
		}
		anim.SetFloat("speed", rb.velocity.magnitude);
	}
}
