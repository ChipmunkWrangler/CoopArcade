using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAround : MonoBehaviour {
	Rect worldspaceScreenBounds;
	Renderer rend;

	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		worldspaceScreenBounds = new Rect {
			min = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, gameObject.transform.position.z)),
			max = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, gameObject.transform.position.z))
		};
	}

	void OnBecameInvisible() {
		Vector3 pos = gameObject.transform.position;
		Bounds bounds = rend.bounds;
		if (pos.x > worldspaceScreenBounds.xMax) {
			pos.x = worldspaceScreenBounds.xMin - bounds.extents.x;
		} else if (pos.x < worldspaceScreenBounds.xMin) { 
			pos.x = worldspaceScreenBounds.xMax + bounds.extents.x;
		}
		if (pos.y > worldspaceScreenBounds.yMax) {
			pos.y = worldspaceScreenBounds.yMin - bounds.extents.y;
		} else if (pos.y < worldspaceScreenBounds.yMin) { 
			pos.y = worldspaceScreenBounds.yMax + bounds.extents.y;
		}
		gameObject.transform.position = pos;

	}

}
