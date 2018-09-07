using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSubmode : Submode {
	[SerializeField] GameObject face;
	[SerializeField] Transform leftEye;
	[SerializeField] Transform rightEye;
	[SerializeField] GameObject lookAtMe;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {		
		gameObject.SetActive (true);
		face.SetActive (true);
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		gameObject.SetActive (false);
		face.SetActive (false);
	}

	void Update() {
		if (lookAtMe.activeInHierarchy) {
			TurnEyeTo (leftEye, lookAtMe.transform);
			TurnEyeTo (rightEye, lookAtMe.transform);
		}
	}

	void TurnEyeTo(Transform eye, Transform tgt) {
		Vector3 eyePos = eye.position;
		eyePos.z = tgt.position.z + 10f;
		eye.rotation = Quaternion.LookRotation(eyePos - tgt.position);
	}

}
