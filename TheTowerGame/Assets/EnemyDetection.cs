using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


            public class EnemyDetection : MonoBehaviour {
	                    public bool gameWon = false;
	                    public float movementSpeed = 0.01f;
	                    GameObject[] opposingObjectPeople;
	                    GameObject[] opposingFriendlySmallTower;
	public static GameObject[] opposingFriendlyLargeTower;
	                    GameObject nearestenemy;
	                    GameObject nearestTower;
	                    public float distanceStartTargetingPlayer = 5f;
	                    public float distanceToStopFromTarget = 10f;
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
	    float distanceOfFinalTower;
	                    int target;
	                    bool notTargetingPlayer = true;
	                    float angle1;
	                    float angle2;
	                    float angle3;
	public int fireRate;
	public float meleeDamage = 0.1f;
	int hitRate = 20;
	int countHit = 0;
	public Animator anim;
	int towerDestroyCount = 0;

	float currentHealth;

	void Start(){
		currentHealth = health;
	}
	                    void OnCollisionEnter2D(Collision2D c){
		                            if (c.gameObject.tag == "enemyweapon") {
			                                    TakeDamage (c.gameObject.GetComponent<BulletBehavior>().damage);
			                                }
		                        }


	void avoidObstacle(Collision2D c){
		GameObject target = getTarget();
		Vector3 direction = target.transform.position - transform.position;
		direction.Normalize ();
		RaycastHit2D hit = Physics2D.Raycast (transform.position, direction);
		float rotZ = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler(0f,0f,rotZ + 90);
		Debug.Log (c.gameObject.tag);
		if ((c.gameObject.tag == "friendly" || c.gameObject.tag == "smallFriendlyTower" || c.gameObject.tag == "largeFriendlyTower") && (hit.collider.tag == "friendly" || hit.collider.tag == "smallFriendlyTower" || hit.collider.tag == "largeFriendlyTower")) {
			if ((rotation.eulerAngles.z < 315 && rotation.eulerAngles.z > 225) || (rotation.eulerAngles.z < 135 && rotation.eulerAngles.z > 45)) {
				if (transform.position.y < c.transform.position.y) {
					transform.position += -transform.up * Time.deltaTime * 4;
				} else {
					transform.position += transform.up * Time.deltaTime * 4;
				}


			} else {
				if (transform.position.x < c.transform.position.x) {
					transform.position += -transform.right * Time.deltaTime * 4;
				} else {
					transform.position += transform.right * Time.deltaTime * 4;

				}
			}

		}
	}

	void OnCollisionStay2D(Collision2D c){
		avoidObstacle (c);


		if (c.gameObject.tag == "enemy" ) {
			countHit++;
			if (countHit > hitRate) {
				TakeDamage (c.gameObject.GetComponent<FriendlyDetection> ().meleeDamage);
				countHit = 0;
				if (anim != null) {
					anim.SetTrigger ("start");
				}
			}
		}
		if (c.gameObject.tag == "smallEnemyTower" || c.gameObject.tag == "largeEnemyTower" ) {
			countHit++;
			if (countHit > hitRate) {
				if (anim != null) {
					anim.SetTrigger ("start");
				}
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

	                    public GameObject getTarget() {


		                            if (target == 0) {
			                                    return nearestenemy;
		                            } 

									if (target == 1) {
			                                    return nearestTower;
		                            } 
								
									if (target == 2) {
										if(opposingFriendlyLargeTower.Length > 0){
										return opposingFriendlyLargeTower [0];
											} else {
										return null;
										}
								
										} 

		return null;
									
		                        }



	                    void Update () {


		            healthbar.fillAmount = currentHealth/health;

		                            notTargetingPlayer = true;
		                            shortestDistance = 10000;
		                            towerShortestDistance = 10000;

		                            opposingObjectPeople = GameObject.FindGameObjectsWithTag("enemy");
		                            opposingFriendlySmallTower = GameObject.FindGameObjectsWithTag("smallEnemyTower");
		                            opposingFriendlyLargeTower = GameObject.FindGameObjectsWithTag("largeEnemyTower");

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

		        distanceOfFinalTower = Mathf.Sqrt( Mathf.Pow(opposingFriendlyLargeTower[0].transform.position.x - posx, 2) + Mathf.Pow(opposingFriendlyLargeTower[0].transform.position.y - posy,2) );

		if (shortestDistance < distanceStartTargetingPlayer && towerShortestDistance > shortestDistance) {
			notTargetingPlayer = false;
			target = 0;

			if (shortestDistance > distanceToStopFromTarget) {
				angle1 = Mathf.Atan2 ((opposingObjectPeople [record].transform.position.y - transform.position.y), (opposingObjectPeople [record].transform.position.x - transform.position.x));
				transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle1) * movementSpeed), transform.position.y + (Mathf.Sin (angle1) * movementSpeed));
			}

		} else if (opposingFriendlySmallTower.Length >= 1 && notTargetingPlayer == true) {

			target = 1;

			if (towerShortestDistance > distanceToStopFromTarget && opposingFriendlySmallTower.Length > 1) {
				angle2 = Mathf.Atan2 ((opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y), (opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x));
				transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle2) * movementSpeed), transform.position.y + (Mathf.Sin (angle2) * movementSpeed));
			} else {

				if (towerShortestDistance < distanceOfFinalTower && towerShortestDistance > distanceToStopFromTarget) {
					angle2 = Mathf.Atan2 ((opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y), (opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x));
					transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle2) * movementSpeed), transform.position.y + (Mathf.Sin (angle2) * movementSpeed));
				} else {
					if (distanceOfFinalTower > distanceToStopFromTarget) {
						angle3 = Mathf.Atan2 ((opposingFriendlyLargeTower [0].transform.position.y - transform.position.y), (opposingFriendlyLargeTower [0].transform.position.x - transform.position.x));
						transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle3) * movementSpeed), transform.position.y + (Mathf.Sin (angle3) * movementSpeed));
					}
				} 
			}

		} else {

			target = 2;

			if (distanceOfFinalTower > distanceToStopFromTarget) {
				angle3 = Mathf.Atan2 ((opposingFriendlyLargeTower [0].transform.position.y - transform.position.y), (opposingFriendlyLargeTower [0].transform.position.x - transform.position.x));
				transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle3) * movementSpeed), transform.position.y + (Mathf.Sin (angle3) * movementSpeed));
			}

		} 
		          

		                        }
}
            
        

