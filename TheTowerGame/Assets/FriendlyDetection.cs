using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyDetection : MonoBehaviour {

	public bool gameWon = false;
	public float movementSpeed;
	GameObject[] opposingObjectPeople;
	GameObject[] opposingFriendlySmallTower;
	GameObject[] opposingFriendlyLargeTower;
	GameObject nearestenemy;
	GameObject nearestTower;
	public float distanceStartTargetingPlayer;
	float posx; 
	float posy;
	float posz;
	float targetx;
	float targety;
	float targetz;
	float towerx;
	float towery;
	float towerz;
	float degrees;
	float shortestDistance;
	float towerShortestDistance;
	int recordSmallTower;
	int record;
	float full = 2;
	public float health = 1f;
	public Image healthbar;

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
		healthbar.fillAmount = health;

		shortestDistance = 10000;
		towerShortestDistance = 10000;

		opposingObjectPeople = GameObject.FindGameObjectsWithTag("friendly");
		opposingFriendlySmallTower = GameObject.FindGameObjectsWithTag("smallFriendlyTower");
		opposingFriendlyLargeTower = GameObject.FindGameObjectsWithTag("largeFriendlyTower");

		posx = transform.position.x; 
		posy = transform.position.y;
		posz = transform.position.z;

		for (int i = 0; i < opposingObjectPeople.Length; i++) {
			targetx = opposingObjectPeople[i].transform.position.x; 
			targety = opposingObjectPeople[i].transform.position.y;
			targetz = opposingObjectPeople[i].transform.position.z;

			var distance = Mathf.Sqrt( Mathf.Pow(targetx - posx, 2) + Mathf.Pow(targety - posy,2) );

			if (distance < shortestDistance) {
				shortestDistance = distance;
				nearestenemy = opposingObjectPeople[i];
				record = i;
			}
		}

		for (int j = 0; j < opposingFriendlySmallTower.Length; j++) {
			towerx = opposingFriendlySmallTower[j].transform.position.x; 
			towery = opposingFriendlySmallTower[j].transform.position.y;
			towerz = opposingFriendlySmallTower[j].transform.position.z;

			var towerDistance = Mathf.Sqrt( Mathf.Pow(towerx - posx, 2) + Mathf.Pow(towery - posy,2) );

			if (towerDistance < towerShortestDistance) {
				towerShortestDistance = towerDistance;
				nearestTower = opposingFriendlySmallTower[j];
				recordSmallTower = j;
			}
		}

		if (shortestDistance < distanceStartTargetingPlayer ) {

			if (opposingObjectPeople [record].transform.position.y - transform.position.y > 0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y + movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y < -0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y - movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y < -0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y - movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y > 0.3 && opposingObjectPeople [record].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y + movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y == 0 && opposingObjectPeople [record].transform.position.x - transform.position.x > 0) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y);
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y == 0 && opposingObjectPeople [record].transform.position.x - transform.position.x < 0) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y);
			} else if (opposingObjectPeople [record].transform.position.y - transform.position.y > 0 && opposingObjectPeople [record].transform.position.x - transform.position.x == 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + movementSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - movementSpeed);
			}

		} else if (opposingFriendlySmallTower != null) {

			if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y > 0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y + movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y < -0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y - movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y < -0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y - movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y > 0.3 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y + movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y == 0 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x > 0) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y);
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y == 0 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x < 0) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y);
			} else if (opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y > 0 && opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x == 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + movementSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - movementSpeed);
			}

		} else if(opposingFriendlyLargeTower != null) {

			if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y > 0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y + movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y < -0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x > 0.3) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y - movementSpeed);
				if (GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = false;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y < -0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y - movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y > 0.3 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x < -0.3) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y + movementSpeed);
				if (!GetComponent<SpriteRenderer> ().flipX) {
					GetComponent<SpriteRenderer> ().flipX = true;
				}
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y == 0 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x > 0) {
				transform.position = new Vector2 (transform.position.x + movementSpeed, transform.position.y);
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y == 0 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x < 0) {
				transform.position = new Vector2 (transform.position.x - movementSpeed, transform.position.y);
			} else if (opposingFriendlyLargeTower [0].transform.position.y - transform.position.y > 0 && opposingFriendlyLargeTower [0].transform.position.x - transform.position.x == 0) {
				transform.position = new Vector2 (transform.position.x, transform.position.y + movementSpeed);
			} else {
				transform.position = new Vector2 (transform.position.x, transform.position.y - movementSpeed);
			} 
		} else {
			gameWon = true;
		}

	}

}
