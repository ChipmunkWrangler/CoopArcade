using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCatSubmode : Submode {
	[SerializeField] GameObject cat;
	[SerializeField] GameObject catBehaviour;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		if (moveShip != null) {
			PickupCoins pickupScript = moveShip.gameObject.GetComponent<PickupCoins> ();
			if (pickupScript) {
				pickupScript.coinsAreHazards = true;
			}
		}
		cat.SetActive (true);
		cat.SendMessage ("OnSetBehaviour", catBehaviour);
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		if (moveShip != null) {
			PickupCoins pickupScript = moveShip.gameObject.GetComponent<PickupCoins> ();
			if (pickupScript) {
				pickupScript.coinsAreHazards = false;
			}
		}
		cat.SetActive (false);
	}
}
