using UnityEngine;
using UnityEngine.InputSystem;

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
		// Attempt to shoot
		inventory.PrimaryGun.Shoot(transform);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// Get the Interactable componenent from object getting triggered
		Interactable interactable = collision.GetComponent<Interactable>();

		// If the interactable component is present
		if(interactable)
        {
			// Is interaction key being pressed 
			bool interactionStatus = controls.Player.Ineract.triggered;

			// Is interact key is pressed
			interactable.Interact(gameObject, interactionStatus);
        }
	}
}
