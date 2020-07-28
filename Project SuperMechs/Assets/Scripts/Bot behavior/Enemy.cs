using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Shootable
{
	[Tooltip("Speed at which enemy moves")]
	public float speed = 0.5f;

	[Tooltip("The health of the enemy")]
	public float health = 20;

	[Tooltip("Damage applied by the enemy to the target")]
	public float DamagePerSecond = 10;

	/// <summary>
	/// Current target to follow
	/// </summary>
	private GameObject CurrentTarget;

	/// <summary>
	/// Instance of the rigidbody 2d component
	/// </summary>
	private Rigidbody2D rb2d;

	private void Start()
	{
		// The target at the start is the player
		CurrentTarget = GameObject.FindGameObjectWithTag("Player");

		// Get the rigidbody 2d component
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		// Look at the current target
		LookAt2D.LookAt(transform, CurrentTarget.transform.position);

		// Move in the forward direction (updated by the look at function above) with the given speed
		rb2d.MovePosition((Vector3)rb2d.position + transform.up * speed * Time.deltaTime);
	}

	/// <summary>
	/// While being shot at, handle the request
	/// </summary>
	/// <param name="Damage"> The damage to apply </param>
	/// <param name="ProjectileDiirection"> The projectiles direction </param>
	public override void OnShotAt(float Damage, Vector3 ProjectileDiirection)
	{
		// reduce health by given damage values
		health -= Damage;

		// when the health value goes below zero
		if (health <= 0)
		{
			// Trigger an enemy death event
			SpawnManager.OnEnemyDeath?.Invoke(gameObject);
			
			// destroy this game object
			Destroy(gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		// While colliding with an object tagged player
		if(collision.gameObject.CompareTag("Player"))
		{
			// Get the health component
			Health playerHealth = collision.gameObject.GetComponent<Health>();

			// If playerHealth wasn't null due to some designer mistake
			if (playerHealth != null)
			{
				// Apply damage by depleting health
				playerHealth.Deplete(DamagePerSecond);
			}
		}
	}


}
