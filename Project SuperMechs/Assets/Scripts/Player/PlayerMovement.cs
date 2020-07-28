using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Tooltip("Movement Speed")]
	public float MovementSpeed = 3.0f;

	/// <summary>
	/// rigidbody of the 2d object
	/// </summary>
	private Rigidbody2D rb2d;

	/// <summary>
	/// A reference to the player controls
	/// </summary>
	private Controls controls;

	private void Awake()
	{
		controls = new Controls();
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
		rb2d = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		// Get the movement input value
		Vector2 inputValue = controls.Player.MovementAction.ReadValue<Vector2>();

		// Apply the input value as force with the magnitude of movement speed
		rb2d.MovePosition(rb2d.position + inputValue * MovementSpeed * Time.fixedDeltaTime);
	}
}
