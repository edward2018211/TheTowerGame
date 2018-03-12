using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDirectionForEnemy : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
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
	void Start () {

	}
	public float rotationOffset = 90;

	void Awake(){

		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No Firepoint");
		} else {
			Debug.Log ("This gun is working");
		}



	}

	// Update is called once per frame
	void Update () {

		shortestDistance = 10000;

		gameobjects = GameObject.FindGameObjectsWithTag("friendly"); //need a second version for the enemy

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

		if (shortestDistance < 15f) {
			Shoot ();
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
		Debug.Log ("Works"); 
		Vector2 mousePosition = new Vector2 (nearestEnemyPoint.position.x, nearestEnemyPoint.position.y );
		Vector2 firePointPrecision = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPrecision, mousePosition - firePointPrecision, 100, whatToHit);
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			Debug.Log ("Validated");
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
	}

}
