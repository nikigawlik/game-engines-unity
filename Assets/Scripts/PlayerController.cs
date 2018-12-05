using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[Header("Moving")]
	[SerializeField] private float speed = 2f;
	[SerializeField] private float speedModifier = 1f;
	[Header("Shooting")]
	[SerializeField] private float bulletSpeed = 7f;
	[SerializeField] private float bulletDelay = .3f;
	[SerializeField] private float bulletSpawnOffset = .1f;
	[SerializeField] private float bulletSpread = .1f;
	[SerializeField] private float knockback = 1f;
	[SerializeField] private ObjectPool bulletPool;

	private Animator animator;
	private Rigidbody2D rigidbody2d;
	
	// shoot direction we keep track of
	private Vector2 shootDirection = Vector2.right;
	private float bulletCountdown = 0f;

	private void Start() {
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		// moving
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = input.normalized;

		if(input.magnitude > 0.01) {
			// get move angle
			float moveAngle = Mathf.Atan2(input.y, input.x);
			// round it to 90°
			float shootAngle = Mathf.Round(moveAngle / (0.5f * Mathf.PI)) * 0.5f * Mathf.PI;
			// Only set shoot direction when we are moving non-diagonally
			if (
				Mathf.Abs(Mathf.DeltaAngle(shootAngle, moveAngle)) < 40f * Mathf.Deg2Rad &&
				!Input.GetButton("Fire1")
			) {
				shootDirection = new Vector2(Mathf.Cos(shootAngle), Mathf.Sin(shootAngle));
			}
			animator.SetFloat("moveX", shootDirection.x);
			animator.SetFloat("moveY", shootDirection.y);
		}
		
		rigidbody2d.velocity = input * speed * speedModifier;
		animator.SetFloat("speed", rigidbody2d.velocity.magnitude);

		// shooting
		if(Input.GetButton("Fire1") && bulletCountdown == 0f) {
			GameObject bullet = bulletPool.GetObject();
			bullet.tag = "Player";
            Quaternion bulletDirection = Quaternion.Euler(0, 0, (
				Mathf.Rad2Deg * Mathf.Atan2(shootDirection.y, shootDirection.x)
				+ Random.Range(-bulletSpread, bulletSpread))
			);
			bullet.transform.position = transform.position + bulletDirection * (new Vector3(bulletSpawnOffset, 0, 0));
            bullet.transform.rotation = bulletDirection;
			bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * Vector3.right * bulletSpeed;
			bullet.GetComponent<Projectile>().shooter = this.gameObject;

			// knockback
			rigidbody2d.velocity += -shootDirection * bulletSpeed * knockback;

			bulletCountdown = bulletDelay;
			animator.SetTrigger("fire");
		}
		bulletCountdown = Mathf.Max(bulletCountdown - Time.fixedDeltaTime, 0f);
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

	private void OnValidate() {
		Debug.Assert(UnityEditor.PrefabUtility.GetPrefabType(gameObject) != UnityEditor.PrefabType.PrefabInstance || bulletPool != null, this.gameObject);
	}
}
