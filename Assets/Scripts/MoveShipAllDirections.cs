using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class MoveShipAllDirections : MonoBehaviour {
	public float maxSpeed;

	void Update () {
		Vector2 velocity = new Vector2 (CnInputManager.GetAxis ("HorizontalA") + CnInputManager.GetAxis ("HorizontalB"), CnInputManager.GetAxis ("VerticalA") + CnInputManager.GetAxis ("VerticalB"));
		if (velocity.sqrMagnitude > 0) {
			gameObject.transform.right = Vector2.right;
			gameObject.transform.Translate (velocity * maxSpeed * Time.deltaTime);
			gameObject.transform.right = velocity;
		}
	}
}
