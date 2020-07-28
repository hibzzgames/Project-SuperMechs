using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
	/// <summary>
	/// Private reference to the controls
	/// </summary>
	private Controls controls;

	/// <summary>
	/// Private reference to the PlayerInput component attached to a player
	/// </summary>
	private PlayerInput playerInput;

	/// <summary>
	/// The plane at which the player exists in 2d top down space. 
	/// </summary>
	/// <remarks>
	/// Used for raycasting to a plane
	/// </remarks>
	private Plane playerPlane;

	private Camera playerCamera;

	private void Awake()
	{
		// Create new controls that the script can refer to
		controls = new Controls();

		// Get the PlayerInput component attached to the object
		playerInput = GetComponent<PlayerInput>();
	}

	private void OnEnable()
	{
		// Enable the use of the private control reference
		controls.Enable();
	}

	private void OnDisable()
	{
		// Disable the use of the private control reference
		controls.Disable();
	}

	private void Start()
	{
		// Create a plane based on current player position
		playerPlane = new Plane(transform.forward, Vector3.zero);

		// Set the player camera to be the main camera
		playerCamera = Camera.main;
	}

	private void FixedUpdate()
	{
		// If the current control scheme uses keyboard and mouse,
		// use the mouse logic to control player rotation
		if (playerInput.currentControlScheme.Equals("Keyboard and Mouse"))
		{
			// Build a ray from camera through the current mouse position
			Ray mouseRay = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

			// Variable to store the hit distance from camera to the plane
			float hitDistance;

			// If the ray was able to hit the plane, update the hit distance variable with the new value
			if (playerPlane.Raycast(mouseRay, out hitDistance))
			{
				// We get hit position by tracking the distance from the origin of the ray along the direction
				Vector3 target = mouseRay.GetPoint(hitDistance);

				// Make the player look at this target
				LookAt2D.LookAt(transform, target);
			}
		}
		// If the current control scheme uses a gamepad
		// use the gamepad stick value, or whatever's configured for gamepad look direction
		// to control player rotation
		else if (playerInput.currentControlScheme.Equals("Gamepad"))
		{
			// We reed the look direction vector 2 input
			Vector3 rightStickValue = controls.Player.LookDirection.ReadValue<Vector2>();

			// We create a target one unit away from the player position in the input direction
			Vector2 target = transform.position + rightStickValue;

			// Make the player look at this target
			LookAt2D.LookAt(transform, target);
		}
	}
}
