using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManger : MonoBehaviour
{
	[Tooltip("Reference to ammo gameobject")]
	public GameObject ammo = null;

	[Header("Powerups")]
	[Tooltip("Reference to the Divide Powerup gameobject")]
	public GameObject dividePowerup = null;

	[Tooltip("Reference to the Battery Powerup prefab")]
	public GameObject batteryPowerup = null;

	[Header("Other properties")]
	[Tooltip("The explosion intensity when the loot drops")]
	public float lootExplosionIntensity = 2.5f;

	/// <summary>
	/// Simple zero quaternion
	/// </summary>
	private Quaternion zeroQuaternion;

	private void Start()
	{
		// initialize zero quaternion value
		zeroQuaternion = Quaternion.Euler(0, 0, 0);
	}

	private void OnEnable()
	{
		// Spawn loot on Enemy Death Event
		SpawnManager.OnEnemyDeath += SpawnLoot;
	}

	private void OnDisable()
	{
		// unsubscribe to enemy death event
		SpawnManager.OnEnemyDeath -= SpawnLoot;
	}

	/// <summary>
	/// Spawn Loot from the given spawn source
	/// </summary>
	/// <param name="SpawnSource"> The gameobject that spawns the loot </param>
	private void SpawnLoot(GameObject SpawnSource)
	{
		// Generate a random number
		int randomNumber = Random.Range(0, 100);

		// 10% chance to spawn a divide powerup
		if(randomNumber < 10)
		{
			randomNumber = Random.Range(0, 2);
			if(randomNumber == 0)
			{
				SpawnItem(dividePowerup, SpawnSource);
			}
			else
			{
				SpawnItem(batteryPowerup, SpawnSource);
			}
			
		}

		// Generate random number
		int ammoCount = Random.Range(1, 5);
		
		// Spawn 1-5 ammo
		while(ammoCount > 0)
		{
			ammoCount--;
			SpawnItem(ammo, SpawnSource);
		}
	}

	/// <summary>
	/// Spawn's an item fromt the spawn source 
	/// </summary>
	/// <param name="item"> The item to spawn </param>
	/// <param name="SpawnSource"> The gameobject that spawn's the item </param>
	private void SpawnItem(GameObject item, GameObject SpawnSource)
	{
		// Instantiate item at the SpawnSources's position
		Vector3 spawnPosition = SpawnSource.transform.position;
		GameObject loot = Instantiate(item, spawnPosition, zeroQuaternion);

		// Get the rigidbidy andd apply random force
		Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();
		rb.AddForce(RandomUtility.RandomForce(lootExplosionIntensity), ForceMode2D.Impulse);
	}	
}
