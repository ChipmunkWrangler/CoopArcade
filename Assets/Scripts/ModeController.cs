using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour {
	[SerializeField] GameObject[] uiContainers;
	[SerializeField] Mode[] modes;
	[SerializeField] string[] titles;
	[SerializeField] GameController gameController;
	[SerializeField] UnityEngine.UI.Text modeText;
	[SerializeField] Slider thrustSlider;
	[SerializeField] Slider torqueSlider;
	[SerializeField] GameObject gun;

	int modeIdx;
	string prefsKey = "modeIdx";
	string thrustKey = "thrustMultiplier";
	string torqueKey = "torqueMultiplier";		

	public void UpdateThrustFromSlider () {
		float thrust = thrustSlider.value;
		UnityEngine.PlayerPrefs.SetFloat (titles[modeIdx] + thrustKey, thrust);
		modes[modeIdx].SetThrustMultiplier(thrust);
	}

	public void UpdateTorqueFromSlider () {
		float torque = torqueSlider.value;
		UnityEngine.PlayerPrefs.SetFloat (titles[modeIdx] + torqueKey, torque);
		modes[modeIdx].SetTorqueMultiplier(torque);
	}

	public void NextMode() {
		DeactivateMode (modeIdx);
		modeIdx++;
		if (modeIdx >= uiContainers.Length) {
			modeIdx = 0;
		}
		UnityEngine.PlayerPrefs.SetInt (prefsKey, modeIdx);
		ActivateMode (modeIdx);
	}

	void Start() {
		modeIdx = UnityEngine.PlayerPrefs.GetInt (prefsKey, modeIdx) % modes.Length;
		UnityEngine.Assertions.Assert.AreEqual (uiContainers.Length, modes.Length);
		UnityEngine.Assertions.Assert.AreEqual (uiContainers.Length, titles.Length);
		for (int i = 0; i < uiContainers.Length; ++i) {
			DeactivateMode (i);
		}
		ActivateMode (modeIdx);
	}

	void DeactivateMode(int idx) {
		uiContainers [idx].SetActive (false);
		modes [idx].StopMode ();
		modeText.text = "";
		gun.SetActive (false);
	}

	void ActivateMode(int idx) {
		uiContainers [idx].SetActive (true);
		modes [idx].StartMode ();
		modeText.text = titles [idx];
		float thrustMultiplier = thrustSlider.isActiveAndEnabled ? UnityEngine.PlayerPrefs.GetFloat (titles [modeIdx] + thrustKey, 1f) : 1f;
		thrustSlider.value = thrustMultiplier;
		modes[modeIdx].SetThrustMultiplier(thrustMultiplier);
		float torqueMultiplier = torqueSlider.isActiveAndEnabled ? UnityEngine.PlayerPrefs.GetFloat (titles [modeIdx] + torqueKey, 1f) : 1f;
		torqueSlider.value = torqueMultiplier;
		modes[modeIdx].SetTorqueMultiplier(torqueMultiplier);
	}
}
