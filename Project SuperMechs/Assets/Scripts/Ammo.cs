using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Loot
{
	// Ammo Pickup event
	public delegate void AmmoPickupAction();
	public static event AmmoPickupAction OnAmmoPickup;

	/// <summary>
	/// Loot base override: On Ammo Pickup
	/// </summary>
	public override void OnPickedUp()
	{
		// Trigger the event
		OnAmmoPickup?.Invoke();
	}
}
