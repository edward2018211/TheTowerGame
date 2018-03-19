using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_camera : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	float updatePosx;
	float updatePosy;
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - player.transform.position;
		if (Camera.main.ScreenToWorldPoint (Input.mousePosition).x > player.transform.position.x) {
			updatePosx = player.transform.position.x + mousePos.x / 2.25f;

		} else {
			updatePosx = player.transform.position.x + mousePos.x / 10f;

		}


			updatePosy = player.transform.position.y + mousePos.y / 2.25f;

		

		updatePosx = Mathf.Max(8.8f,updatePosx);
		updatePosx = Mathf.Min (updatePosx,36f);
		updatePosy = Mathf.Max(-20f,updatePosy);
		updatePosy = Mathf.Min (updatePosy,20f);
		transform.position = new Vector3 (updatePosx, updatePosy , -10);

	}
}
