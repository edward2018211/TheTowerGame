using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArmmelee : MonoBehaviour {

	// Use this for initialization
	float rotZ;
	public float rotationOffset = 90;
	GameObject target;
	public Animator anim;

	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
		
		target = GetComponentInParent<EnemyDetection>().getTarget();
		Vector3 difference = target.transform.position - transform.position;
		difference.Normalize();
		rotZ = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f,0f,rotZ + rotationOffset);
		if (transform.rotation.eulerAngles.z < 180 && transform.rotation.eulerAngles.z > 0) {
			transform.rotation = Quaternion.Euler(0,0,rotZ + rotationOffset);
		} else {
			rotZ = Mathf.Atan2(difference.x,difference.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0,180,rotZ + rotationOffset + 90);
		}
		
	}
}
