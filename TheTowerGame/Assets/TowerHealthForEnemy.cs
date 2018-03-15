using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthForEnemy : MonoBehaviour {

	float currentHealth;
	public float health = 1f;
	public Image healthBar;
	float full = 2;
	public float meleeDamage = 0.1f;
	float countHit;
	float hitRate = 35;

	void Start(){
		currentHealth = health;
	}


	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.tag == "friendlyweapon") {
			TakeDamage (c.gameObject.GetComponent<BulletBehavior>().damage);
		}
	}

	void OnCollisionStay2D(Collision2D c){
		if (c.gameObject.tag == "friendly") {
			countHit++;
			if (countHit > c.gameObject.GetComponent<EnemyDetection>().fireRate) {
				TakeDamage (c.gameObject.GetComponent<EnemyDetection> ().meleeDamage);
				countHit = 0;
			}
		}
	}


	public void TakeDamage(float amount) {
		currentHealth -= amount;
		//healthBar.fillAmount = currentHealth / 100;
		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
		healthBar.fillAmount = currentHealth / health;
	}
}
