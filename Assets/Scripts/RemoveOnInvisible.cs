﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnInvisible : MonoBehaviour {
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
