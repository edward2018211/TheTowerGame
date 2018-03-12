using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStream : MonoBehaviour {

	//public int playerHealth;       // Reference to the player's heatlh.
	public GameObject bullet;                // The enemy prefab to be spawned.
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

		Instantiate(bullet, transform.position, transform.rotation);

		if (stopSpawning) {
			CancelInvoke ("Spawn");
		}

	}  

}
