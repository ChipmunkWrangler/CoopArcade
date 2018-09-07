using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInvisible : Submode {
	[SerializeField] GameObject ship;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		GameObject colliderObj = ship.GetComponentInChildren<Collider> ().gameObject;
		ship.GetComponent<Collider2D> ().enabled = false;
		ship.GetComponentInChildren<Collider> ().enabled = false;
		ship.GetComponent<Renderer> ().enabled = false;
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		ship.GetComponent<Collider2D> ().enabled = true;
		ship.GetComponentInChildren<Collider> ().enabled = true;
		ship.GetComponent<Renderer> ().enabled = true;
	}
}
