using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class SteerShotSubmode : Submode {
	[SerializeField] Transform ship;
	[SerializeField] GameObject shotPrefab;
	[SerializeField] float muzzleSpeed;
	[SerializeField] bool alwaysMove;

	GameObject activeShot;
	Vector2 oldVelocity;
	bool isActive;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		gameObject.SetActive (true);
		isActive = true;
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		gameObject.SetActive (false);
		Destroy (activeShot);
		isActive = false;
	}
		
	void Update() {
		if (!isActive) { 
			return;
		}
		Vector2 velocity;
		if (alwaysMove) {
			Vector2 dirA = new Vector2 (CnInputManager.GetAxis ("HorizontalShootA"), CnInputManager.GetAxis ("VerticalShootA"));
			Vector2 dirB = new Vector2 (CnInputManager.GetAxis ("HorizontalShootB"), CnInputManager.GetAxis ("VerticalShootB"));
			if (dirA != Vector2.zero) {
				dirA.Normalize ();
			} 
			if (dirB != Vector2.zero) {
				dirB.Normalize ();
			} 
			velocity = dirA + dirB;
			if (velocity == Vector2.zero) {
				velocity = oldVelocity;
			}
		} else {
			velocity = new Vector2 (CnInputManager.GetAxis ("HorizontalShootA") + CnInputManager.GetAxis ("HorizontalShootB"), CnInputManager.GetAxis ("VerticalShootA") + CnInputManager.GetAxis ("VerticalShootB"));
		}
		if (velocity != Vector2.zero) {
			if (!activeShot) {
				activeShot = (GameObject)Instantiate (shotPrefab, ship.position, Quaternion.identity);
			}
			activeShot.transform.right = Vector2.right;
			activeShot.transform.Translate (velocity * muzzleSpeed * Time.deltaTime);
			activeShot.transform.up = velocity;
			oldVelocity = velocity;
		}
	}
}
