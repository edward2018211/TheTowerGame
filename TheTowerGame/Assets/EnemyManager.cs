using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	//public int playerHealth;       // Reference to the player's heatlh.
	public GameObject enemy;                // The enemy prefab to be spawned.
	public bool stopSpawning = false;
	public float spawnTime;            
	public float spawnDelay;        


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnDelay);
	}


	public void Spawn ()
	{
		// Index to spawn enemies.
		//int spawnPointIndex = spawnPoints.Length; //Change spawn point here
		Instantiate(enemy, transform.position, transform.rotation);

		if (stopSpawning) {
			CancelInvoke ("Spawn");
		}
			
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		//Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}  
}
