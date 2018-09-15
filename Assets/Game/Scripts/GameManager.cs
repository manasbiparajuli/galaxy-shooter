// Handle game management

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public bool gameOver = true;
	public bool isCoopMode = false;

	[SerializeField] private GameObject _player;
	[SerializeField] private GameObject _coopPlayers;
	[SerializeField] private GameObject _pauseMenuPanel;

	private Animator _pauseAnimator;
	private UIManager _uiManager;
	private SpawnManager _spawnManager;

	private void Start()
	{
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		_pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();

		// Ensure that the animation does not pause when we pause the game
		_pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
	}

	// Handle game over and pause mode
	private void Update()
	{
		if (gameOver)
		{
			// Let player play game again if "Space" key is pressed
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// Instantiate players depending on game mode
				if (!isCoopMode)
				{
					Instantiate(_player, Vector3.zero, Quaternion.identity);
				}
				else
				{
					Instantiate(_coopPlayers, Vector3.zero, Quaternion.identity);
				}
				gameOver = false;
				_uiManager.HideTitleScreen();
				_spawnManager.StartSpawnRoutines();
			}

			// Switch to game menu when "Esc" is pressed
			else if (Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene("Main_Menu");
			}
		}
		
		// Pause the game
		if (Input.GetKeyDown(KeyCode.P))
		{
			_pauseMenuPanel.SetActive(true);
			_pauseAnimator.SetBool("isPaused", true);

			Time.timeScale = 0;
		}
	}

	// Remove the pause panel from the screen when game is resumed
	public void ResumeGame()
	{
		_pauseMenuPanel.SetActive(false);
		Time.timeScale = 1f;
	}
}