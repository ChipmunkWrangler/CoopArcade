using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour {
	[SerializeField] GameObject replacement;
	[SerializeField] minMaxFloatPair replacementSpeedRange;
	public Transform replacementParent;
	public bool destructible;

	void OnCollisionEnter (Collision col) 
	{
		if (col.gameObject.CompareTag ("Player")) {
			HitPlayer (col.transform.parent.gameObject);
		} else if (!col.gameObject.CompareTag (gameObject.tag) && !col.gameObject.CompareTag("Indestructible")) {
			Destroy (col.gameObject);
		}
	}

	void OnTriggerEnter (Collider col) 
	{
		if (col.gameObject.CompareTag ("Player")) {
			HitPlayer (col.transform.parent.gameObject);
		} else if (col.gameObject.CompareTag ("Shot") && destructible) {
			if (replacement) {
				SpawnReplacement (replacement, gameObject.GetComponent<Rigidbody>().velocity);
			}
			Destroy (gameObject);
		}
	}

	void HitPlayer(GameObject player) {
		player.SendMessage ("OnHit", SendMessageOptions.DontRequireReceiver);
	}

	void SpawnReplacement(GameObject o, Vector2 velocity) {
		GameObject newO = (GameObject)Instantiate (o, gameObject.transform.position, o.transform.rotation, replacementParent);
		Rigidbody2D rb = newO.GetComponent<Rigidbody2D> ();
		if (rb) {
			float multiplier = 1f;
			if (velocity.magnitude > replacementSpeedRange.max) {
				multiplier = replacementSpeedRange.max / velocity.magnitude;
			} else if (velocity.magnitude < replacementSpeedRange.min) {
				multiplier = replacementSpeedRange.min / velocity.magnitude;
			}
			rb.velocity = velocity * multiplier;
		}
	}
}
