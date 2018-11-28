using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerController : MonoBehaviour {
	[SerializeField] private float speed;
	private Rigidbody2D rb;
	private Animator anim;
	
	[Header("Obstacle avoidance")]
	public float avoidDistance = 1f;
	public float directionChangeThreshold = 0.1f;
	public string[] layersToAvoid;

	private int avoidLayerMask;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		avoidLayerMask = LayerMask.GetMask(layersToAvoid);
	}

	private void FixedUpdate() {
		PlayerController player = GameController.Instance.player;
		if(player != null) {
            Vector3 vectorToPlayer = (player.transform.position - transform.position).normalized;
			Vector3 vectorToPlayerTangent = Quaternion.Euler(0, 0, 90) * vectorToPlayer;
			Vector3 potentialMoveDirection = vectorToPlayer; 
			float[] offsets = new float[] {-1, 1, 0};

			foreach(int offset in offsets) {
				float radius = GetComponentInChildren<CircleCollider2D>().radius;
				Vector3 origin = transform.position + vectorToPlayerTangent * offset * radius;
				RaycastHit2D hit = Physics2D.Raycast(origin, vectorToPlayer, avoidDistance, avoidLayerMask);
				Debug.DrawRay(origin, vectorToPlayer, Color.red);
				if(hit.collider != null) {
					Vector3 tangent = Quaternion.Euler(0, 0, 90) * hit.normal;
					tangent = Vector3.Dot(vectorToPlayer, tangent) * tangent;

					tangent.Normalize();
					Debug.DrawRay(hit.point, tangent, Color.red);

					potentialMoveDirection = tangent;
				}
			}

            rb.velocity = potentialMoveDirection * speed;
		
			anim.SetFloat("lookX", vectorToPlayer.x);
		}
		anim.SetFloat("speed", rb.velocity.magnitude);
	}
}
