using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Commander : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( EnemyDetection.opposingFriendlyLargeTower.Length == 0 ){
			LostGame ();
		}
	}

	public void LostGame() {

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);

	}
}
