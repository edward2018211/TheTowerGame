using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_arm : MonoBehaviour {

	// Use this for initialization
	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
	float timeToFire = 0;
	Transform firePoint;
	public Transform MuzzleFlashPrefab;
	float rotZ;
	public float rotationOffset = 90;

	void Start () {
		
	}


	void Awake(){
		firePoint = transform.Find ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No Firepoint");
		}
	}

	// Update is called once per frame
	void Update () {

		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize();
		 rotZ = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f,0f,rotZ + rotationOffset);
		if (transform.rotation.eulerAngles.z < 180 && transform.rotation.eulerAngles.z > 0) {
			transform.rotation = Quaternion.Euler(0,0,rotZ + rotationOffset);
		} else {
			 rotZ = Mathf.Atan2(difference.x,difference.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0,180,rotZ + rotationOffset + 90);
		}


		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			} 
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time +  1/fireRate;
				Shoot ();
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
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPrecision = new Vector2 ( firePoint.position.x, firePoint.position.y );
		RaycastHit2D hit = Physics2D.Raycast (firePointPrecision, mousePosition - firePointPrecision, 100, whatToHit );
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}

		//Debug.DrawLine (firePointPrecision, (mousePosition-firePointPrecision) * 100 );
		if (hit.collider != null) {
			//Debug.DrawLine (firePointPrecision, hit.point, Color.red);
			//Debug.Log ("We Hit " + hit.collider.name + "and did " + Damage + " damage.");
		}
	}

}
