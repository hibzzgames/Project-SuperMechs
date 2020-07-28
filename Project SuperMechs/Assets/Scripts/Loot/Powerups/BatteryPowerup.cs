using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BatteryPowerup : Loot
{
	[Tooltip("The health amount restored")]
	[SerializeField]
	private float HealthRestored = 300;

	#region events

	public delegate void BatteryPickupAction(float healthToRestore);
	public static event BatteryPickupAction OnBatteryPickup;

	#endregion

	/// <summary>
	/// Set the health restored value of the battery
	/// </summary>
	/// <param name="value"> The amount of health restored on being picked up </param>
	public void SetHealthRestored(float value)
	{
		HealthRestored = value;
	}

	/// <summary>
	/// On battery pickup
	/// </summary>
	public override void OnPickedUp()
	{
		// Triggers a battery pickup
		OnBatteryPickup?.Invoke(HealthRestored);
	}
}
