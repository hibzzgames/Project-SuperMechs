using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Max health of the player")]
	private float MaxHealth = 100;

	[SerializeField]
	[Tooltip("Can this health component be recharged by picking up batteries? ")]
	private bool RechargeUsingBattery = false;

	/// <summary>
	/// Current Health of the player
	/// </summary>
	private float CurrentHealth = 100;

	[Header("UI Elements")]
	[SerializeField]
	private TMP_Text HealthText = null;

	[SerializeField]
	private Slider HealthSlider = null;

	#region Events
	public delegate void HealthReachZeroAction();
	public static event HealthReachZeroAction OnHealthReachZero;
	#endregion

	public void Start()
	{
		// Initialize current health to be max health at start
		CurrentHealth = MaxHealth;

		// Update slider max value to current max health
		HealthSlider.maxValue = MaxHealth;

		// Update health UI
		UpdateHealthUI();
	}

	/// <summary>
	/// Deplete the health of the gameobject by the given dps
	/// </summary>
	/// <param name="DamagePerSecond">Damage per second applied </param>
	public void Deplete(float DamagePerSecond)
	{
		// Reduce health by given value based on time
		CurrentHealth -= DamagePerSecond * Time.deltaTime;

		// Check health and update UI
		CheckHealth();
		UpdateHealthUI();
	}

	/// <summary>
	/// Check if health is below or equal to zero
	/// </summary>
	/// <returns> If true, health is below zero. Else false </returns>
	public bool CheckHealth()
	{
		// Check if current health is below zero 
		if (CurrentHealth <= 0)
		{
			// set it to zero
			CurrentHealth = 0;

			// Trigger an health reach zero event
			OnHealthReachZero?.Invoke();
			return true;
		}

		return false;
	}

	/// <summary>
	/// Update the health UI
	/// </summary>
	public void UpdateHealthUI()
	{
		// Set appropriate slider value
		HealthSlider.value = CurrentHealth;

		// Update health text to current health
		HealthText.text = CurrentHealth.ToString("0") + " / " + MaxHealth.ToString("0");
	}

	/// <summary>
	/// Adds health to the current health value
	/// </summary>
	/// <param name="healthToAdd"> The amount of health to add </param>
	public bool AddHealth(float healthToAdd)
	{
		

		// Add the given value to current health
		CurrentHealth += healthToAdd;

		// Current health shouldnt be greater than max health
		if(CurrentHealth > MaxHealth)
		{
			// So we clamp it to max health
			CurrentHealth = MaxHealth;
		}

		// Update tthe health UI reflecting changes made
		UpdateHealthUI();

		return true;
	}

	public bool AddHealthBattery(float healthToAdd)
    {
		// If the health compoenent cannot pickup batteries, don't recharge by adding it
		if (!RechargeUsingBattery) return false;

		// If it reaches here, the health component can be recharged with a battery
		// So add the health
		AddHealth(healthToAdd);

		// Indicate that it has been recharged
		return true;

	}
}
