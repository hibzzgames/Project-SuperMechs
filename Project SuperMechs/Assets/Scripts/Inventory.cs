using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[Tooltip("The primary gun of the held in the inventory")]
	public Gun PrimaryGun = null;

	[Tooltip("The primary ammo count")]
	public int AmmoCount = 0;

	[Header("UI Elements")]
	[SerializeField]
	private TMP_Text AmmoText = null;

	private void Start()
	{
		// Update ammo UI at start
		UpdateAmmoUI();
	}

	private void OnEnable()
	{
		// Link event handlers
		Ammo.OnAmmoPickup += HandleAmmoPickup;
	}

	private void OnDisable()
	{
		// unlink event handlers
		Ammo.OnAmmoPickup -= HandleAmmoPickup;
	}

	/// <summary>
	/// Decrement the AmmoCount by the given value
	/// </summary>
	/// <param name="ammo"> Count of ammo to reduce by </param>
	public void DecrementAmmo(int ammo)
	{
		AmmoCount -= ammo;
		UpdateAmmoUI();
	}

	/// <summary>
	/// Increment the AmmoCount by the given value
	/// </summary>
	/// <param name="ammo"> Count of ammo to increase by </param>
	public void IncrementAmmo(int ammo)
	{
		AmmoCount += ammo;
		UpdateAmmoUI();
	}

	/// <summary>
	/// Update the ammo UI
	/// </summary>
	public void UpdateAmmoUI()
	{
		AmmoText.text = "Ammo: " + AmmoCount;
	}

	/// <summary>
	/// Handle the ammo pickup event
	/// </summary>
	private void HandleAmmoPickup()
	{
		IncrementAmmo(1);
	}
}