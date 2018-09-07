using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHazards : Submode {
	[SerializeField] float initialWaitInSeconds;
	[SerializeField] minMaxIntPair hazardsPerWave;
	[SerializeField] GameObject hazard;
	[SerializeField] float waveGapInSeconds;
	[SerializeField] float spawnGapInSeconds;
	[SerializeField] int maxHazards = -1;
	[SerializeField] Transform parent;
	[SerializeField] bool destructible;
	[SerializeField] Transform replacementParent;

	override public void StartSubmode (MoveShipRotateAndThrust moveShip) {
		StartCoroutine(SpawnWaves());
	}
		
	override public void StopSubmode (MoveShipRotateAndThrust moveShip) {
		StopAllCoroutines();
		for (int i = 0; i < parent.childCount; ++i) {
			Destroy(parent.GetChild(i).gameObject);
		}
		for (int i = 0; i < replacementParent.childCount; ++i) {
			Destroy(replacementParent.GetChild(i).gameObject);
		}

	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(initialWaitInSeconds);
		do {
			yield return StartCoroutine (SpawnWave (Random.Range (hazardsPerWave.min, hazardsPerWave.max)));
			if (waveGapInSeconds >=0 ) {
			yield return new WaitForSeconds (waveGapInSeconds);
			} else {
				break;
			}
		} while (destructible);
	}

	IEnumerator SpawnWave(int numToSpawn) {
		for (int i = 0; i < numToSpawn; ++i) {
			if (maxHazards < 0 || parent.childCount < maxHazards) {
				SpawnHazard ();
			}
			yield return new WaitForSeconds(spawnGapInSeconds);
		}
	}

	void SpawnHazard() {
		int side = Random.Range (0, 4);
		Vector2 spawnPosition = Get2DOffCameraPos (side);
		GameObject newHazard = (GameObject) Instantiate(hazard, spawnPosition, Quaternion.identity, parent);
		newHazard.GetComponent<Renderer> ().material.color = Random.ColorHSV(0,1f,1f,1f,0.5f,1f);
		newHazard.GetComponent<Tumble> ().SetDir (GetDir (side));
		newHazard.GetComponent<AsteroidCollision> ().destructible = destructible;
		newHazard.GetComponent<AsteroidCollision> ().replacementParent = replacementParent;
	}

	Vector2 Get2DOffCameraPos(int side) {
		switch (side) {
		case 0: // Left
			return Camera.main.ScreenToWorldPoint (new Vector3 ( 0,                                            Camera.main.pixelHeight * Random.Range(0, 1f)));
		case 1: // Right
			return Camera.main.ScreenToWorldPoint (new Vector3 ( Camera.main.pixelWidth,                       Camera.main.pixelHeight * Random.Range(0, 1f)));
		case 2: // Top
			return Camera.main.ScreenToWorldPoint (new Vector3 ( Camera.main.pixelWidth * Random.Range(0, 1f), 0));
		case 3: // Bottom
			return Camera.main.ScreenToWorldPoint (new Vector3 ( Camera.main.pixelWidth * Random.Range(0, 1f), Camera.main.pixelHeight));
		}
		return Vector3.zero;
	}

	Vector2 GetDir(int side) {
		switch (side) {
		case 0: // Left
			return new Vector2(1f, 0);
		case 1: // Right
			return new Vector2(-1f, 0);
		case 2: // Top
			return new Vector2(0, 1f);
		case 3: // Bottom
			return new Vector2(0, -1f);
		}
		return Vector2.zero;
	}
}
