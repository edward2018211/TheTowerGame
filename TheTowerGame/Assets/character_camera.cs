using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_camera : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	float updatePos;
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - player.transform.position;
		if (Camera.main.ScreenToWorldPoint (Input.mousePosition).y > player.transform.position.y) {
			updatePos = player.transform.position.y + mousePos.y / 2.25f;

		} else {
			updatePos = player.transform.position.y + mousePos.y / 10f;

		}
		updatePos = Mathf.Max(8.8f,updatePos);
		updatePos = Mathf.Min (updatePos,36f);
		transform.position = new Vector3 (0.64f, updatePos, -10);

	}
}
