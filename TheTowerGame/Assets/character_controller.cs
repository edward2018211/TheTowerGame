using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class character_controller : NetworkBehaviour {

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
	public Image[] dockItems;
	public GameObject[] holograms;
	public GameObject[] units;
	int currentItem = 1;
	int countHit;
	int hitRate = 10;
	float distanceToMouse;
	public float maxSpawnDistance;



	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.tag == "enemyweapon") {
			TakeDamage (c.gameObject.GetComponent<BulletBehavior>().damage);
		}
	}

	void OnCollisionStay2D(Collision2D c){
		if (c.gameObject.tag == "enemy") {
			countHit++;
			if (countHit > hitRate) {
				TakeDamage (c.gameObject.GetComponent<FriendlyDetection> ().meleeDamage);
				countHit = 0;
			}
		}
	}

	public void TakeDamage(float amount) {
		health -= amount;
		if (health <= 0) {
			Die ();
		}
	}

	void Die(){
		
		Destroy (gameObject);
	}




	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}



		distanceToMouse = Vector3.Distance (new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x,Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0), transform.position);

		if (buildMode) {
			crosshair.GetComponent<SpriteRenderer> ().enabled = false;
			for (int i = 0; i < dockItems.Length;i++){
				if (currentItem == i + 1) {
					
					dockItems [i].color = new Color32 (255, 255, 255, 255);

					holograms [i].GetComponent<SpriteRenderer> ().enabled = true;

					if (distanceToMouse > maxSpawnDistance) {
						holograms [i].GetComponent<SpriteRenderer> ().color = new Color32 (255, 0, 0, 120);
					} else {
						holograms [i].GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 120);
					}

				} else {
					dockItems [i].color = new Color32 (255, 255, 255, 80);
					holograms [i].GetComponent<SpriteRenderer> ().enabled = false;

				}




			}
		} else {
			crosshair.GetComponent<SpriteRenderer> ().enabled = true;
			for (int i = 0; i < dockItems.Length;i++){
					dockItems [i].color = new Color32 (255, 255, 255, 80);	
					holograms [i].GetComponent<SpriteRenderer> ().enabled = false;

			}

		}

		for (int i = 0; i < holograms.Length; i++) {
			holograms [i].transform.position = new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x,Camera.main.ScreenToWorldPoint (Input.mousePosition).y,0);

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

		if (Input.GetKeyDown("1")){
			currentItem = 1;
		}
		if (Input.GetKeyDown("2")){
			currentItem = 2;
		}
		if (Input.GetKeyDown("3")){
			currentItem = 3;
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
			} else {
				if (currentItem == 2) {
					if (energy >= 40 && distanceToMouse < maxSpawnDistance) {
						Instantiate (units [currentItem - 1], new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, 0), transform.rotation);

						energy -= 40;
					}
				}
				if (currentItem == 1) {
					if (energy >= 40 && distanceToMouse < maxSpawnDistance) {
						Instantiate (units [currentItem - 1], new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, 0), transform.rotation);

						energy -= 40;
					}
				}
			}
		}



	
}

}