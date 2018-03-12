using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3 (GetComponentInParent<Transform>().position.x - Camera.main.ScreenToWorldPoint (Input.mousePosition).x,0,-10);

	}
}
