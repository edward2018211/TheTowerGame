using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour {

	float currentHealth;
	public float health = 1f;
	public Image healthbar;
	float full = 2;

	void Start(){
		currentHealth = health;
	}


	void OnCollisionEnter2D(Collision2D c){

		if (this.gameObject.tag == "smallFriendlyTower" || this.gameObject.tag == "largeFriendlyTower") {
			if (c.gameObject.tag == "enemyweapon") {
				TakeDamage (c.gameObject.GetComponent<BulletBehavior> ().damage);
			}
		} 

		if(this.gameObject.tag == "smallEnemyTower" || this.gameObject.tag == "largeEnemyTower"){
			if (c.gameObject.tag == "friendlyweapon") {
				TakeDamage (c.gameObject.GetComponent<BulletBehavior> ().damage);
			}
		}
	}

	public void TakeDamage(float amount) {
		currentHealth -= amount;
		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.fillAmount = currentHealth / health;
	}
}
