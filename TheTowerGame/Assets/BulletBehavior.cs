using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	// Use this for initialization
	public float range = 60;
	int counter;
	public float damage = 10;
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D c){
		
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		counter++;

		if (counter > range) {
			Destroy (gameObject);
		}

		transform.position += transform.right * Time.deltaTime * 50;

	}
}
