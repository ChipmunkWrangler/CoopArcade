using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIndestructibleSubmode : Submode {
	[SerializeField] GameObject ship;
	string oldTag;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		GameObject colliderObj = ship.GetComponentInChildren<Collider> ().gameObject;
		oldTag = colliderObj.tag;
		colliderObj.tag = "Indestructible";
//		ship.GetComponent<Collider2D> ().enabled = false;
//		ship.GetComponentInChildren<Collider> ().enabled = false;
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		if (oldTag != null) {
			ship.GetComponentInChildren<Collider> ().gameObject.tag = oldTag;
			oldTag = null;
		}
//		ship.GetComponent<Collider2D> ().enabled = true;
//		ship.GetComponentInChildren<Collider> ().enabled = true;
	}
}
