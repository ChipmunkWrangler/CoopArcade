using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnHit : MonoBehaviour {
	void OnHit() {
		gameObject.SetActive (false);
	}
}
