using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class MoveShipRotateAndThrust : MonoBehaviour {
	public float torque;
	public float thrust;
	public float thrustMultiplier { protected get; set; }
	public float torqueMultiplier { protected get; set; }

	Rigidbody2D rb;
	bool rotatingCW;
	bool rotatingCCW;
	bool thrusting;

	public void SetCW(bool b) {
		rotatingCW = b;
	}
	public void SetCCW(bool b) {
		rotatingCCW = b;
	}
	public void SetThrust(bool b) {
		thrusting = b;
	}

	void Start() {
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		if (rotatingCW) {
			RotateCW (torque * torqueMultiplier);
		} 
		if (rotatingCCW) {
			RotateCCW (torque * torqueMultiplier);
		} 
		RotateCW(CnInputManager.GetAxis ("Horizontal") * torque * torqueMultiplier);


		Thrust(CnInputManager.GetAxis ("Vertical") * thrust * thrustMultiplier);
		if (thrusting) {
			Thrust (thrust * thrustMultiplier);
		} 
	}

	public void Cancel() {
		rotatingCW = false;
		rotatingCCW = false;
		thrusting = false;
	}

	void RotateCCW(float _torque) {
		rb.AddTorque (_torque);
	}
	void RotateCW(float _torque) {
		rb.AddTorque (-_torque);
	}
	void Thrust(float _thrust) {
		rb.AddForce (gameObject.transform.right * _thrust);
	}

}
