using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTimer : Submode {
	[SerializeField] minMaxFloatPair switchTimeRange;
	[SerializeField] GameObject[] uiA;
	[SerializeField] GameObject[] uiB;
	bool isActiveUIB;
	Coroutine timer;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		UpdateUIActiveState ();
		timer = StartCoroutine(SwitchAfterTime());
		moveShip.SetThrust (true);
	}
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		if (timer != null) {
			StopCoroutine (timer);
			timer = null;
		}
		moveShip.SetThrust (false);
	}

	IEnumerator SwitchAfterTime() {
		while (true) {
			yield return new WaitForSeconds (Random.Range (switchTimeRange.min, switchTimeRange.max));
			isActiveUIB = !isActiveUIB;
			UpdateUIActiveState ();
		}
	}

	void UpdateUIActiveState() {
		SetActiveUI (uiA, !isActiveUIB);
		SetActiveUI (uiB, isActiveUIB);
	}

	void SetActiveUI(GameObject[] objs, bool b) {
		foreach (GameObject o in objs) {
//			moveShip.Cancel ();
			o.SetActive (b);
		}
	}
}
