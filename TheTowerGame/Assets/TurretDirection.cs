﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDirection : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
	public float distanceStartTargeting = 15f;
	float timeToFire = 0;
	Transform nearestEnemyPoint;
	Transform firePoint;
	public Transform MuzzleFlashPrefab;
	GameObject[] gameobjects; 
	float degrees;
	GameObject nearestenemy;
	//EnemyDetection enemy;
	float posx; 
	float posy;
	float posz;
	float targetx;
	float targety;
	float targetz;
	float shortestDistance;
	int record;
	int u = 0;
	public float damage = 10f;
	public float range = 1000f;
	float health = 2f;
	float rotZ;
	public GameObject bullet;
	float count;
	void Start () {

	}
	public float rotationOffset = 90;

	void Awake(){
		
		firePoint = transform.Find ("FirePoint");  

	}

	// Update is called once per frame
	void Update () {

		count++;



		shortestDistance = 10000;

		gameobjects = GameObject.FindGameObjectsWithTag("enemy"); //need a second version for the enemy

		posx = transform.position.x; 
		posy = transform.position.y;
		posz = transform.position.z;

		for (int i = 0; i < gameobjects.Length; i++) {
			targetx = gameobjects[i].transform.position.x; 
			targety = gameobjects[i].transform.position.y;
			targetz = gameobjects[i].transform.position.z;

			var distance = Mathf.Sqrt( Mathf.Pow(targetx - posx, 2) + Mathf.Pow(targety - posy,2) );

			if (distance < shortestDistance) {
				shortestDistance = distance;
				nearestenemy = gameobjects[i];
				nearestEnemyPoint = nearestenemy.transform;
				record = i;
			}

		}


			

		if (shortestDistance < distanceStartTargeting) {

			Vector3 difference =  (nearestenemy.transform.position) - transform.position;
			difference.Normalize();
			rotZ = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f,0f,rotZ + rotationOffset);
			if (transform.rotation.eulerAngles.z < 180 && transform.rotation.eulerAngles.z > 0) {
				transform.rotation = Quaternion.Euler(0,0,rotZ + rotationOffset);
			} else {
				rotZ = Mathf.Atan2(difference.x,difference.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0,180,rotZ + rotationOffset + 90);
			}
			if (count > fireRate) {
				Shoot ();
				count = 0;
			}

		}




	}

	void Effect(){
		Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
	}

	void Shoot(){ 
		Vector2 mousePosition = new Vector2 (nearestEnemyPoint.position.x, nearestEnemyPoint.position.y );
		Vector2 firePointPrecision = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPrecision, mousePosition - firePointPrecision, 100, whatToHit);
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
		Instantiate (bullet, firePoint.transform.position, Quaternion.Euler(firePoint.transform.rotation.eulerAngles.x,firePoint.transform.rotation.eulerAngles.y,firePoint.transform.rotation.eulerAngles.z - 90));
	}





}

