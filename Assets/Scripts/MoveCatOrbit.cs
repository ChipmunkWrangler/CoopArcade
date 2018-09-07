using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class MoveCatOrbit : MoveCat {
	[SerializeField] float extendSpeed;
	[SerializeField] float rotationSpeed;
	[SerializeField] minMaxFloatPair distToShipRange;

	float distToShip;

	void OnEnable() {
		distToShip = distToShipRange.min;
		cat.position = (Vector2)ship.position + Vector2.right * distToShip;
	}

	void Update () {
		float desiredFractionOfMax = Mathf.Clamp (CnInputManager.GetAxis ("VerticalCat"), 0, 1f);
		float desiredDistToShip = distToShipRange.GetFraction (desiredFractionOfMax);
		distToShip = Mathf.MoveTowards (distToShip, desiredDistToShip, extendSpeed * Time.deltaTime);
		Vector2 normal = cat.position - ship.position;
 		cat.position = (Vector2)ship.position + normal.normalized * distToShip;
		Vector2 dir = Quaternion.Euler (0, 0, 90f) * normal;
		cat.Translate (dir * rotationSpeed * Time.deltaTime / distToShip);
//		cat.position = ship.position + new Vector3 (Mathf.Sin (Time.time * rotationSpeed), Mathf.Cos (Time.time * rotationSpeed)) * distToShip;
	}
}
