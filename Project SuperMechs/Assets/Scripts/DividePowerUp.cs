using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividePowerUp : Loot
{
	/// <summary>
	/// On Divide powerup being picked up
	/// </summary>
	public override void OnPickedUp()
	{
		// Triggers an event responsible for updating the multiplier of the SpawnManagar
		SpawnManager.ModifyMultiplier?.Invoke(0.5f);
	}
}
