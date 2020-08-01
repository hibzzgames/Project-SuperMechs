using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
	/// <summary>
	/// Virtual function representing on pickup
	/// </summary>
	/// <param name="picker"> The game object that picks up the object </param>
	public virtual void OnPickedUp(GameObject picker) { }

	[Header("Loot Properties")]
	[Tooltip("Time the loots stay in-game")]
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

	/// <summary>
	/// This function is called when an object interacts with this Interactable
	/// </summary>
	/// <param name="Interactii"> The object that interacts with Interactable </param>
	public override void OnInteract(GameObject Interactii)
    {
		// Handle on being picked up
		OnPickedUp(Interactii);

		// Destroy the object upon being picked up
		Destroy(gameObject);
    }
}
