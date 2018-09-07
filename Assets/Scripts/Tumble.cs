using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumble : MonoBehaviour {

	public minMaxFloatPair rotationSpeedRange;
	public minMaxFloatPair speedRange;

	public void SetDir(Vector2 dir) {
		GetComponent<Rigidbody> ().velocity = dir * Random.Range (speedRange.min, speedRange.max);
	}

	void Start ()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * Random.Range (rotationSpeedRange.min, rotationSpeedRange.max);
	}
}
