using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	[SerializeField] GameObject coinContainer;
	[SerializeField] GameObject ship;

	void Update () {
		if (coinContainer.transform.childCount == 0 || !ship.activeInHierarchy) {
			Restart ();
		}
	}
				
	void Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
