using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Tooltip("Movement Speed")]
	public float MovementSpeed = 10.0f;

	[Tooltip("Player inventory")]
	public Inventory inventory;

	/// <summary>
	/// Private variable storing local position
	/// </summary>
	private Vector3 position;

	/// <summary>
	/// Camera associated with the player
	/// </summary>
	private Camera playerCamera;

	private void Start()
	{
		position = transform.position;
		playerCamera = Camera.main;
	}

	private void FixedUpdate()
	{
		#region Player Movement
		// Player input vertical
		if (Input.GetKey(KeyCode.W))
		{
			position.y += MovementSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			position.y -= MovementSpeed * Time.deltaTime;
		}

		// Player input horizontal
		if (Input.GetKey(KeyCode.D))
		{
			position.x += MovementSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			position.x -= MovementSpeed * Time.deltaTime;
		}

		// Apply the new input position to the player's translation
		transform.position = position;

		// Make player look at the mouse posiition
		LookAt2D.LookAt(transform, playerCamera.ScreenToWorldPoint(Input.mousePosition));
		#endregion

		#region Mouse click

		// When primary button is clicked and when inventory's ammo is greater thant 0
		if(Input.GetMouseButton(0) && inventory.AmmoCount > 0)
		{
			// Attempt to shoot
			bool shotResult = inventory.PrimaryGun.Shoot(transform);
			
			// If the shot was successful, aka, a projectile was spawned
			if(shotResult)
			{
				// decrease the ammo count in the inventory by 1
				inventory.DecrementAmmo(1);
			}
		}

		#endregion
	}
}
