using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
	[SerializeField] Transform behaviours;
	[SerializeField] protected GameObject ship;
	[SerializeField] float timeToSpin;
	[SerializeField] minMaxFloatPair torqueRange;


	GameObject activeBehaviour;
	float spinUntilTime;

	void OnSetBehaviour(object behaviour) {
		for(int i = 0; i < behaviours.childCount; ++i) {
			behaviours.GetChild (i).gameObject.SetActive (false);
		}
		activeBehaviour = (GameObject)behaviour;
		activeBehaviour.SetActive (true);

	}

	void OnHit() {
//		ship.SetActive (false);
		if (spinUntilTime < Time.time) {
			var torque = Random.Range (torqueRange.min, torqueRange.max);
			var rb = gameObject.GetComponent<Rigidbody2D> ();
			rb.angularVelocity = 0;
			rb.AddTorque (torque);
			spinUntilTime = Time.time + timeToSpin;
			activeBehaviour.SetActive (false);
		}
	}

	void Update() {
		if (activeBehaviour && !activeBehaviour.activeSelf && spinUntilTime < Time.time) {
			var pos = gameObject.transform.position;
			activeBehaviour.SetActive(true);
			gameObject.transform.position = pos;
		}
	}
}
