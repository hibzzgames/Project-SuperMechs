using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManger : MonoBehaviour
{
	[Header("Powerup Properties")]
	[Tooltip("References to available powerups at start")]
	public List<GameObject> AvailablePowerups = null;

	[Tooltip("The percentage chance to drop a powerup upon eliminating an enemy")]
	[Range(0, 100)]
	public float PowerupDropChance = 10;

	[Header("Weapon Properties")]
	[Tooltip("References to available weapons at start")]
	public List<GameObject> AvailableWeapons = null;

	[Tooltip("The percentage chance to drop a weapon upon eliminating an enemy")]
	[Range(0,100)]
	public float WeaponDropChance = 5;

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

		// % chance to drop a powerup given by a variable 
		if(randomNumber < PowerupDropChance)
		{
			randomNumber = Random.Range(0, AvailablePowerups.Count);
			SpawnItem(AvailablePowerups[randomNumber], SpawnSource);
		}

		// generate another random number
		randomNumber = Random.Range(0, 100);

		// % chance to drop a weapon specfied by a variable
		if(randomNumber < WeaponDropChance)
        {
			randomNumber = Random.Range(0, AvailableWeapons.Count);
			SpawnItem(AvailableWeapons[randomNumber], SpawnSource);
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
