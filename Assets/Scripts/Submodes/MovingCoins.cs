using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCoins : Submode {
	[SerializeField] float waitPerImpulse;
	[SerializeField] minMaxFloatPair forceRange;
	[SerializeField] Transform coinParent;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		coinParent.gameObject.SetActive (true);
		StartCoroutine(MoveCoins());

	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		StopAllCoroutines();
		if (coinParent.gameObject.activeSelf) {
			for (int i = 0; i < coinParent.childCount; ++i) {
				Destroy (coinParent.GetChild (i).gameObject);
			}
			coinParent.gameObject.SetActive (false);
		}
	}

	IEnumerator MoveCoins() {
		while (true) {
			for (int i = 0; i < coinParent.childCount; ++i) {
				Rigidbody2D rb = coinParent.GetChild (i).gameObject.GetComponent<Rigidbody2D> ();
				if (rb) {
					rb.AddForce (Random.insideUnitCircle * Random.Range (forceRange.min, forceRange.max), ForceMode2D.Impulse);
				}
			}
			yield return new WaitForSeconds (waitPerImpulse);
		}
	}
}
