using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	[SerializeField] Text text;
	int score;

	public void Inc ()
	{
		++score;
		UpdateText ();
	}

	void Start() {
		UpdateText ();
	}

	void UpdateText() {
		text.text = "Score: " + score;
	}
}
