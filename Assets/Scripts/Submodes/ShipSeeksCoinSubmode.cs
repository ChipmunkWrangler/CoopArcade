using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class ShipSeeksCoinSubmode : Submode {
	[SerializeField] Transform ship;
	[SerializeField] float shipSpeed;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		gameObject.SetActive (true);
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		gameObject.SetActive (false);
	}
		
	void Update() {
		GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");
		float distance = Mathf.Infinity;
		Vector2 diff = Vector2.zero;
		foreach (GameObject coin in coins) {
			diff = coin.transform.position - ship.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				distance = curDistance;
			}
		}
		if (diff.sqrMagnitude > 0) {
			Vector2 dir = diff.normalized;
			ship.transform.right = Vector2.right;
			ship.transform.Translate (dir * shipSpeed * Time.deltaTime);
			ship.transform.right = dir;
		}
	}
}
