using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour {

	public float moveSpeed = 0.01f;
	 
	Transform playerGraphics;
	public GameObject bullet;
	public GameObject firePoint;
	public float health = 100;
	public float energy = 100;
	float energyCounter = 0;
	public float energyRechargeRate = 10;
	bool buildMode = false;
	public GameObject playerArm;
	public GameObject crosshair;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (buildMode) {
			crosshair.GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			crosshair.GetComponent<SpriteRenderer> ().enabled = true;
		}
		
		if (energy < 100) {
			energyCounter++;
		}

		if (energyCounter > energyRechargeRate) {
			energyCounter = 0;
			energy++;
		}

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

		if (Input.GetKeyDown("e")){
			buildMode = !buildMode;
		}
			
		if (Input.GetMouseButtonDown (0)) {
			if (!buildMode) {
				if (energy >= 5) {
					energy -= 5;
					Instantiate (bullet, firePoint.transform.position, Quaternion.Euler (firePoint.transform.rotation.eulerAngles.x, firePoint.transform.rotation.eulerAngles.y, firePoint.transform.rotation.eulerAngles.z - 90));
					playerArm.GetComponent<character_arm> ().Effect ();
				}
			}
		}



	
}

}