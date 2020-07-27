using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Tooltip("Movement Speed")]
	public float MovementSpeed = 3.0f;

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

	/// <summary>
	/// The plane assosciate with the player's orientation
	/// </summary>
	private Plane playerPlane;

	/// <summary>
	/// rigidbody of the 2d object
	/// </summary>
	private Rigidbody2D rb2d;

	/// <summary>
	/// A reference to the player controls
	/// </summary>
	private Controls controls;

	private PlayerInput playerInput;

	private void Awake()
	{
		controls = new Controls();
		controls.Player.PrimaryFire.performed += ctx => AttemptToShoot();

		playerInput = GetComponent<PlayerInput>();
	}

	private void OnEnable()
	{
		// Enable the controls
		controls.Enable();
	}

	private void OnDisable()
	{
		// Disable the controls
		controls.Disable();
	}

	private void Start()
	{
		position = transform.position;
		rb2d = GetComponent<Rigidbody2D>();
		playerCamera = Camera.main;

		// Create a plane based on current player position
		playerPlane = new Plane(transform.forward, Vector3.zero);
	}

	private void FixedUpdate()
	{
		// Get the movement input value
		Vector2 inputValue = controls.Player.MovementAction.ReadValue<Vector2>();

		// Apply the input value as force with the magnitude of movement speed
		rb2d.MovePosition(rb2d.position + inputValue * MovementSpeed * Time.fixedDeltaTime);

		if(playerInput.currentControlScheme.Equals("Keyboard and Mouse"))
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
		else if(playerInput.currentControlScheme.Equals("Gamepad"))
		{
			Vector3 rightStickValue = controls.Player.LookDirection.ReadValue<Vector2>();
			Vector2 target = transform.position + rightStickValue;

			LookAt2D.LookAt(transform, target);
		}

		#region Player Movement
		/*

		// Make player look at the mouse posiition

		// Perspective camera

		// Build a ray from camera through the current mouse position
		Ray mouseRay = playerCamera.ScreenPointToRay(Input.mousePosition);
		
		// Variable to store the hit distance from camera to the plane
		float hitDistance;

		// If the ray was able to hit the plane, update the hit distance variable with the new value
		if(playerPlane.Raycast(mouseRay, out hitDistance))
		{
			// We get hit position by tracking the distance from the origin of the ray along the direction
			Vector3 target = mouseRay.GetPoint(hitDistance);

			// Make the player look at this target
			LookAt2D.LookAt(transform, target);
		}

		// Orthographic camera
		//LookAt2D.LookAt(transform, playerCamera.ScreenToWorldPoint(Input.mousePosition));*/
		#endregion
	}

	/// <summary>
	/// Attempts to shoot the primary weapon
	/// </summary>
	void AttemptToShoot()
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
