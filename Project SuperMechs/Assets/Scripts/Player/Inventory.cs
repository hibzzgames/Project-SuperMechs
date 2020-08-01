using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[Tooltip("The primary gun of the held in the inventory")]
	public Gun PrimaryGun = null;

	/// <summary>
	/// Swap primary weapon with the given weapon
	/// </summary>
	/// <param name="gun"> The gun to swap to </param>
	/// <remarks>
	/// The previous gun gets destroyed in the process
	/// </remarks>
	public void SwapWeapon(Gun gun)
	{
		// Destroy the previous gun
		Destroy(PrimaryGun.gameObject);

		// Set the given gun as the primary gun
		PrimaryGun = gun;

		// Add gun as a child to the inventory
		gun.transform.parent = transform;

		// Set the first child component, which MUST be the visual component,
		// and set it inactive
		gun.transform.GetChild(0).gameObject.SetActive(false);

		// Set the circle collider of the object inactive
		gun.GetComponent<Collider2D>().enabled = false;
	}
}