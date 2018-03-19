using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArm : MonoBehaviour {

	// Use this for initialization
	float rotZ;
	public float rotationOffset = 90;
	GameObject target;
	public float fireRate = 15;
	public float distanceStartTargeting = 10;
	float count = 0;
	public GameObject bullet;
	public GameObject firePoint;
	public GameObject parent;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		count++;
	

		target = GetComponentInParent<EnemyDetection>().getTarget();
		if (Vector3.Distance (target.transform.position, transform.position) <= distanceStartTargeting) {
			if (count > fireRate) {
				Instantiate (bullet, firePoint.transform.position, firePoint.transform.rotation);
				count = 0;
			}
		}


		Vector3 difference = target.transform.position - transform.position;
		difference.Normalize();
		rotZ = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f,0f,rotZ + rotationOffset);

		if (target.transform.position.x > transform.position.x) {
			transform.rotation = Quaternion.Euler(0,0,rotZ + rotationOffset);
			parent.transform.rotation = Quaternion.Euler(parent.transform.rotation.x,0f,parent.transform.rotation.z);

		} else {
			rotZ = Mathf.Atan2(difference.x,difference.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0,180,rotZ + rotationOffset + 90);
			parent.transform.rotation = Quaternion.Euler(parent.transform.rotation.x,180f,parent.transform.rotation.z);

		}

	}
}
