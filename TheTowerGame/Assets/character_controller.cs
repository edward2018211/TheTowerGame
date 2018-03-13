﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour {

	public float moveSpeed = 0.01f;
	 
	Transform playerGraphics;
	public GameObject bullet;
	public GameObject firePoint;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


		if (Input.GetKey("w")) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + moveSpeed);
		}

		if (Input.GetKey("a")) {
			transform.position = new Vector2 (transform.position.x - moveSpeed, transform.position.y );
		}
		if (Input.GetKeyDown("a")) {
					if (!GetComponent<SpriteRenderer>().flipX) {
				GetComponent<SpriteRenderer> ().flipX = true;
			}
		}

		if (Input.GetKey("d")) {
			transform.position = new Vector2 (transform.position.x + moveSpeed, transform.position.y );
		}

		if (Input.GetKeyDown("d")) {
					if (GetComponent<SpriteRenderer>().flipX) {
						GetComponent<SpriteRenderer> ().flipX = false;
					}
		}

		if (Input.GetKey("s")) {
			transform.position = new Vector2 (transform.position.x , transform.position.y - moveSpeed );
		}
			
		if (Input.GetMouseButtonDown (0)) {
			
			Instantiate (bullet, firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.eulerAngles.x,firePoint.transform.rotation.eulerAngles.y,firePoint.transform.rotation.eulerAngles.z - 90));
		}



	
}

}