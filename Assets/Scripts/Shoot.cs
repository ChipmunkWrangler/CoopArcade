using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class Shoot : MonoBehaviour {
	[SerializeField] Transform gun;
	[SerializeField] GameObject shotPrefab;
	[SerializeField] float muzzleSpeed;
	[SerializeField] float timeBetweenShots;
	[SerializeField] float MIN_MAGNITUDE = 0.5f;

	float canShootAfterTime;

	public void Zap() {
		ZapInDir (gun.right);
	}

	void Update () {
		if (!gameObject.activeInHierarchy) {
			return;
		}
		Vector2 dir = new Vector2 (CnInputManager.GetAxis ("HorizontalShoot"), CnInputManager.GetAxis ("VerticalShoot"));
		if (dir.magnitude > MIN_MAGNITUDE) {
			ZapInDir (dir);
		}
		Vector2 aimDir = new Vector2 (CnInputManager.GetAxis ("HorizontalAim"), CnInputManager.GetAxis ("VerticalAim"));
		if (aimDir.magnitude > MIN_MAGNITUDE) {
			Aim (aimDir);
		}

	}

	void ZapInDir(Vector3 dir) {
		if (canShootAfterTime < Time.time) {
			float xOffset = (gun.GetComponent<Collider2D> ().bounds.extents.x + shotPrefab.GetComponent<Renderer> ().bounds.extents.x) * 1.1f;
			GameObject shot = (GameObject)Instantiate (shotPrefab, gun.position + dir * xOffset, shotPrefab.transform.rotation);
			shot.GetComponent<Rigidbody> ().velocity = dir * muzzleSpeed;
			shot.transform.LookAt (shot.transform.position + dir);
			shot.transform.Rotate (new Vector3 (0, 0, 90f));
			canShootAfterTime = Time.time + timeBetweenShots;
		}
	}


	void Aim(Vector3 dir) {
		gun.gameObject.SetActive (true);
		gun.transform.rotation = LookAt2d(dir);
	}

	Quaternion LookAt2d(Vector3 dir) {
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
