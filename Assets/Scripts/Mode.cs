using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mode : MonoBehaviour {
	[SerializeField] GameObject ship;
	[SerializeField] MoveShipRotateAndThrust moveShip;
	[SerializeField] MoveShipAllDirections moveShipAllDir;
	[SerializeField] float thrust; // only relevant when moveShip is set
	[SerializeField] float torque = -1f; // only relevant when moveShip is set
	[SerializeField] float mass = -1f;
	[SerializeField] float maxSpeed; // only relevant when moveShipAllDir is set
	[SerializeField] List<Submode> submodes;
	float oldMass;
	float oldThrust;
	float oldTorque;
	float oldMaxSpeed;

	public void StartMode () {
		ship.transform.position = Vector3.zero;
		ship.transform.rotation = Quaternion.identity;
		var rb = ship.GetComponent<Rigidbody2D> ();
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
		if (mass > 0) { 
			oldMass = rb.mass;
			rb.mass = mass;
		}
		if (moveShip) {
			moveShip.Cancel ();
			if (thrust >= 0) {
				oldThrust = moveShip.thrust;
				moveShip.thrust = thrust;
			}
			if (torque >= 0) {
				oldTorque = moveShip.torque;
				moveShip.torque = torque;
			}
		}
		if (moveShipAllDir) {
			oldMaxSpeed = moveShipAllDir.maxSpeed;
			moveShipAllDir.maxSpeed = maxSpeed;
		}
		foreach (Submode submode in submodes) {
			submode.StartSubmode (moveShip);
		}
	}

	public void StopMode () {
		foreach (Submode submode in submodes) {
			submode.StopSubmode (moveShip);
		}
		if (oldMass > 0) {
			ship.GetComponent<Rigidbody2D> ().mass = oldMass;
		}
		if (moveShip) {
			if (oldThrust > 0) {
				moveShip.thrust = oldThrust;
			}
			if (oldTorque > 0) { 
				moveShip.torque = oldTorque;
			}
		}
		if (moveShipAllDir && oldMaxSpeed > 0) {
			moveShipAllDir.maxSpeed = oldMaxSpeed;
		}
	}

	public void SetThrustMultiplier( float multiplier ) {
		if (moveShip) {
			moveShip.thrustMultiplier = multiplier;
		}
	}

	public void SetTorqueMultiplier( float multiplier ) {
		if (moveShip) {
			moveShip.torqueMultiplier = multiplier;
		}
	}
}
