using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour {
	[SerializeField] Score score;

	public bool coinsAreHazards;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (enabled && col.gameObject.CompareTag ("Coin")) {
			if (coinsAreHazards) {
				gameObject.SetActive (false);
			} else {
				Destroy (col.gameObject);
				score.Inc ();
			}
		}
	}
}
