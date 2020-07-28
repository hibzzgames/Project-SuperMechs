using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	/// <summary>
	/// Virtual function representing on pickup
	/// </summary>
	public virtual void OnPickedUp() { }

	/// <summary>
	/// Time the loots stay in-game
	/// </summary>
	public float timer = 3.0f;

	private void Update()
	{
		// Timer is decreased by deltatime
		timer -= Time.deltaTime;

		// Loots get destroyed when timer goes to zero
		if(timer <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// The player picks the object up the object, and trigger the appropriate function based
			// on inheritence
			OnPickedUp();

			// Destroy the picked up object
			Destroy(gameObject);
		}
	}
}
