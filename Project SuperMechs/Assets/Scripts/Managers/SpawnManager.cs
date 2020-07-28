using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
	#region events
	public delegate void EnemyDeathAction(GameObject EnemyObject);
	public static EnemyDeathAction OnEnemyDeath;

	public delegate void ModifyMultiplierAction(float modValue);
	public static ModifyMultiplierAction ModifyMultiplier;

	#endregion

	[Tooltip("Reference to the enemy prefab")]
	public GameObject EnemyPrefab = null;

	[Tooltip("The enemy border from which they spawn")]
	public float borderValue = 5;

	[Tooltip("Number of enemies to sa")]
	public int initialSpawn = 3;

	[SerializeField]
	[Tooltip("The enemy spawn multiplier ")]
	private float Multiplier = 2.0f;

	[Header("UI Elements")]
	[Tooltip("Reference to the multiplier text")]
	public TMP_Text MultiplierText = null;

	/// <summary>
	/// Number of enemies that need to be spawned
	/// </summary>
	private float enemybalance = 0;

	/// <summary>
	///  Max border value (for enemy spawn)
	/// </summary>
	private float bordermaxValue = 0;

	/// <summary>
	/// Number of enemies on the scene currently
	/// </summary>
	private int enemyCount = 0;

	/// <summary>
	/// Total number of enemies eliminatedd
	/// </summary>
	private int EnemiesEliminated = 0;

	private void Start()
	{
		// Bordermax is 1 greater than specified bordervalue
		bordermaxValue = borderValue + 1;

		// Reset enemies eliminated to 0
		EnemiesEliminated = 0;

		// Loop and spawn enemies
		for(int i = 0; i < initialSpawn; ++i)
		{
			SpawnRandomEnemy();
		}

		// Update multiplier text to current one at start
		UpdateMultiplierText();
	}

	private void OnEnable()
	{
		// subscribe to On Enemy Death Event -
		OnEnemyDeath += ReportDeath;

		// subscribe to Modify Multiplier Event
		ModifyMultiplier += ModifyMultiplierFn;
	}

	private void OnDisable()
	{
		// unsubscribe to On Enemy Death Event 
		OnEnemyDeath -= ReportDeath;

		// unsubscribe to Modify Multiplier Event
		ModifyMultiplier -= ModifyMultiplierFn;
	}

	/// <summary>
	/// To report a death of an enemy object
	/// </summary>
	/// <param name="EnemyObject"> Enemy gameobject </param>
	public void ReportDeath(GameObject EnemyObject)
	{
		// increment enemy balance based on current multiplier
		enemybalance += Multiplier;

		// decrease enemy count bny 1
		enemyCount--;

		// increment enemy eliminated that rouncd by 1
		EnemiesEliminated++;

		// TODO: Reconsider this
		// Increment the multiplier by the value every time an enemy is killed
		Multiplier += 0.05f;

		// Multiplier text is updated
		UpdateMultiplierText();

		// Spawn enemies based on enemy balance
		while (enemybalance >= 1)
		{
			enemybalance -= 1;
			SpawnRandomEnemy();
		}
	}

	/// <summary>
	/// Spawns an enemy on a random position
	/// </summary>
	public void SpawnRandomEnemy()
	{
		// Max enemies on the scene should be 100
		if(enemyCount < 100)
		{
			#region Random position on board
			float x = Random.Range(-bordermaxValue, bordermaxValue);
			float y;

			if (Mathf.Abs(x) > borderValue)
			{
				y = Random.Range(-borderValue, borderValue);
			}
			else
			{
				y = Random.Range(borderValue, bordermaxValue) * (Random.Range(0, 2) == 0 ? -1 : 1);
			}
			#endregion

			// create vector based on random and x and y generated
			Vector2 randomOPosition = new Vector2(x, y);

			// instantiate the enemy at the random position
			Instantiate(EnemyPrefab, randomOPosition, Quaternion.Euler(0, 0, 0));
			
			// increment enemy count representing the instantiation
			enemyCount++;
		}
	}

	/// <summary>
	/// Multiply current muliplier by the given value
	/// </summary>
	/// <param name="val"> The modify value </param>
	private void ModifyMultiplierFn(float val)
	{
		// Muiltiply with modify value
		Multiplier *= val;

		// Make sure that multiplier doesnt go below 1
		if(Multiplier <= 1.0f) { Multiplier = 1.0f; }

		// Update multiplier text
		UpdateMultiplierText();
	}

	/// <summary>
	/// Update multiplier text
	/// </summary>
	private void UpdateMultiplierText()
	{
		MultiplierText.text = "x" + Multiplier.ToString("0.00");
	}

	/// <summary>
	/// Getter function getting number of enemies eliminated
	/// </summary>
	/// <returns> Number of enemies eliminated </returns>
	public int GetEnemiesEliminated()
	{
		return EnemiesEliminated;
	}
}
