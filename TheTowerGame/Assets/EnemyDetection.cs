using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

	GameObject[] gameobjects;
	float degrees;
	GameObject nearestenemy;
	float posx; 
	float posy;
	float posz;
	float targetx;
	float targety;
	float targetz;
	float shortestDistance;
	int record;
	float full = 2;
	public float health = 1f;
	// Update is called once per frame

	public void TakeDamage(float amount) {
		health -= amount;
		if (health == 1f) {
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}

	void Update () {

		shortestDistance = 10000;

		gameobjects = GameObject.FindGameObjectsWithTag("friendly");

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
				record = i;
			}
		}

		if (gameobjects [record].transform.position.y - transform.position.y > 0.3 && gameobjects [record].transform.position.x - transform.position.x > 0.3) {
			transform.position = new Vector2 (transform.position.x + 0.01f, transform.position.y + 0.01f);
			if (GetComponent<SpriteRenderer>().flipX) {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		} else if (gameobjects [record].transform.position.y - transform.position.y < -0.3 && gameobjects [record].transform.position.x - transform.position.x > 0.3) {
			transform.position = new Vector2 (transform.position.x + 0.01f, transform.position.y - 0.01f);
			if (GetComponent<SpriteRenderer>().flipX) {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
		} else if (gameobjects [record].transform.position.y - transform.position.y < -0.3 && gameobjects [record].transform.position.x - transform.position.x < -0.3) {
			transform.position = new Vector2 (transform.position.x - 0.01f, transform.position.y - 0.01f);
			if (!GetComponent<SpriteRenderer>().flipX) {
				GetComponent<SpriteRenderer> ().flipX = true;
			}
		} else if (gameobjects [record].transform.position.y - transform.position.y > 0.3 && gameobjects [record].transform.position.x - transform.position.x < -0.3) {
			transform.position = new Vector2 (transform.position.x - 0.01f, transform.position.y + 0.01f);
			if (!GetComponent<SpriteRenderer>().flipX) {
				GetComponent<SpriteRenderer> ().flipX = true;
			}
		} else if (gameobjects [record].transform.position.y - transform.position.y == 0 && gameobjects [record].transform.position.x - transform.position.x > 0) {
			transform.position = new Vector2 (transform.position.x + 0.01f, transform.position.y );
		} else if (gameobjects [record].transform.position.y - transform.position.y == 0 && gameobjects [record].transform.position.x - transform.position.x < 0) {
			transform.position = new Vector2 (transform.position.x - 0.01f, transform.position.y);
		} else if (gameobjects [record].transform.position.y - transform.position.y > 0 && gameobjects [record].transform.position.x - transform.position.x == 0) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + 0.01f);
		} else {
			transform.position = new Vector2 (transform.position.x, transform.position.y - 0.01f);
		}
	

	}

}
