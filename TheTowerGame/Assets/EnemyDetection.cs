using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


            public class EnemyDetection : MonoBehaviour {
	                    public bool gameWon = false;
	                    public float movementSpeed = 0.01f;
	                    GameObject[] opposingObjectPeople;
	                    GameObject[] opposingFriendlySmallTower;
	                    GameObject[] opposingFriendlyLargeTower;
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
	public float meleeDamage = 0.1f;
	int hitRate = 15;
	int countHit = 0;
	public Animator anim;

	float currentHealth;

	void Start(){
		currentHealth = health;
	}
	                    void OnCollisionEnter2D(Collision2D c){
		                            if (c.gameObject.tag == "enemyweapon") {
			                                    TakeDamage (c.gameObject.GetComponent<BulletBehavior>().damage);
			                                }
		                        }

	void OnCollisionStay2D(Collision2D c){
		if (c.gameObject.tag == "enemy") {
			countHit++;
			if (countHit > hitRate) {
				TakeDamage (c.gameObject.GetComponent<FriendlyDetection> ().meleeDamage);
				countHit = 0;
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
		                            } else if (target == 1) {
			                                    return nearestTower;
		                            } else{
			                                    return opposingFriendlyLargeTower [0];
			                                }
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

		                            if (shortestDistance < distanceStartTargetingPlayer && towerShortestDistance > shortestDistance ) {

			                                    target = 0;
			                                    notTargetingPlayer = false;
			                                    
			                                    if(shortestDistance > distanceToStopFromTarget){
				                                    angle1 = Mathf.Atan2 ((opposingObjectPeople [record].transform.position.y - transform.position.y), (opposingObjectPeople [record].transform.position.x - transform.position.x));
				                                    transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle1) * movementSpeed), transform.position.y + (Mathf.Sin (angle1) * movementSpeed));
				                                    }

			                                }
		                              else if (opposingFriendlySmallTower != null && notTargetingPlayer == true) {
			                                    
			                                    target = 1;
			            if (towerShortestDistance > distanceToStopFromTarget) {
				                angle2 = Mathf.Atan2 ((opposingFriendlySmallTower [recordSmallTower].transform.position.y - transform.position.y), (opposingFriendlySmallTower [recordSmallTower].transform.position.x - transform.position.x));
				                transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle2) * movementSpeed), transform.position.y + (Mathf.Sin (angle2) * movementSpeed));
				            }
			                                       

			                                } 
		                              else if(opposingFriendlyLargeTower != null && opposingFriendlySmallTower == null) {

			                                    target = 2;
			            if (distanceOfFinalTower > distanceToStopFromTarget) {
				                angle3 = Mathf.Atan2 ((opposingFriendlyLargeTower [0].transform.position.y - transform.position.y), (opposingFriendlyLargeTower [0].transform.position.x - transform.position.x));
				                transform.position = new Vector2 (transform.position.x + (Mathf.Cos (angle3) * movementSpeed), transform.position.y + (Mathf.Sin (angle3) * movementSpeed));
				            }

			                            }
		                            else {
			                                    gameWon = true;
			                                }

		                        }
}
            
        

