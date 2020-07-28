using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

/// <summary>
/// This class handles player input regarding abilities
/// </summary>
public class PlayerHandleAbilities : MonoBehaviour
{
	[Tooltip("Player inventory")]
	public Inventory inventory;

	/// <summary>
	/// Private instance of controls to be used by this script
	/// </summary>
	private Controls controls;

	private void Awake()
	{
		controls = new Controls();
		controls.Player.PrimaryFire.performed += ctx => PrimaryFireShoot();
	}

	private void OnEnable()
	{
		// Enable this player's controls
		controls.Enable();
	}

	private void OnDisable()
	{
		// Disable this player's controls
		controls.Disable();
	}

	/// <summary>
	/// Shoot primary weapon
	/// </summary>
	void PrimaryFireShoot()
	{
		// When primary button is clicked and when inventory's ammo is greater thant 0
		if (inventory.AmmoCount > 0)
		{
			// Attempt to shoot
			bool shotResult = inventory.PrimaryGun.Shoot(transform);

			// If the shot was successful, aka, a projectile was spawned
			if (shotResult)
			{
				// decrease the ammo count in the inventory by 1
				inventory.DecrementAmmo(1);
			}
		}
	}
}
