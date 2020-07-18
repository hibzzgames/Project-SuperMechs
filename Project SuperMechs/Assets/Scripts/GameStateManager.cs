using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
	[Tooltip("A collection of Gameplay UI elements")]
	public GameObject GameplayUI;

	[Tooltip("A collection of UI Elements representing the Gameover screen")]
	public GameObject GameOverScreen;

	[Tooltip("A reference to the Spawn manager")]
	public SpawnManager spawnManager = null;

	[Tooltip("Text representing number of enemies eliminated")]
	public TMP_Text EnemiesKilledText = null;


	private void Start()
	{
		// At start
		// Turn on gameplay UI
		GameplayUI.SetActive(true);

		// Turn off gameplay UI
		GameOverScreen.SetActive(false);
	}

	private void OnEnable()
	{
		// Call gameover on health reaching zero event
		Health.OnHealthReachZero += GameOver;
	}

	private void OnDisable()
	{
		// unsubscribe gameover function from health reach zero event
		Health.OnHealthReachZero -= GameOver;
	}

	/// <summary>
	/// Handle gameover state
	/// </summary>
	private void GameOver()
	{
		// Turrn off gameplay UI
		GameplayUI.SetActive(false);

		// Turn on gameober UI
		GameOverScreen.SetActive(true);
		
		// Update enemies killed text by accessing the spawn manager
		EnemiesKilledText.text = spawnManager.GetEnemiesEliminated() + " ENEMIES ELIMINATED";

		// Set timescale to 0 (trick to emulating a pause)
		Time.timeScale = 0;
	}

	/// <summary>
	/// Restarts the game
	/// </summary>
	public void Restart()
	{
		// Set timescale to 1 (assuming that it could be anything)
		Time.timeScale = 1;

		// Reload current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// Quits the application
	/// </summary>
	public void Quit()
	{
		Application.Quit();
	}
}
