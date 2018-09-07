using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class MoveCatShipIsHome : MoveCat {
	[SerializeField] float maxSpeed;

	void OnEnable() {
		cat.position = ship.position;
	}

	void Update () {
		Vector2 velocity = new Vector2 (CnInputManager.GetAxis ("HorizontalCat"), CnInputManager.GetAxis ("VerticalCat"));
		cat.right = Vector2.right;
		if (velocity.sqrMagnitude == 0 && cat.position != ship.position) {
			cat.right = (ship.position - cat.position).normalized;
			cat.position = Vector2.MoveTowards (cat.position, ship.position, maxSpeed * Time.deltaTime);
		} 
		if (velocity.sqrMagnitude > 0) {
			cat.Translate (velocity * maxSpeed * Time.deltaTime);
			cat.right = velocity;
		}
	}
}
