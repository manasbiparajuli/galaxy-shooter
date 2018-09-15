// Handle UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public Sprite[] lives;
	public Image livesImageDisplay;
	public Text scoreText, bestScoreText;
	public int score, bestScore;

	public GameObject titleScreen;

	private void Start()
	{
		// Save player score
		bestScore = PlayerPrefs.GetInt("HighScore", 0);
		// Set high score
		bestScoreText.text = "Best: " + bestScore;
	}

	public void UpdateLives(int currentLives)
	{
		// Update the UI image for player's lives
		livesImageDisplay.sprite = lives[currentLives];
	}	

	public void UpdateScore()
	{
		score += 10;
		scoreText.text = "Score: " + score;
	}

	public void CheckForBestScore()
	{
		// Check if player has got a new high score
		if (score > bestScore)
		{
			bestScore = score;
			PlayerPrefs.SetInt("HighScore", bestScore);
			bestScoreText.text = "Best: " + bestScore;
		}
	}

	public void ShowTitleScreen()
	{
		titleScreen.SetActive(true);
		score = 0;
	}

	public void HideTitleScreen()
	{
		titleScreen.SetActive(false);
		scoreText.text = "Score: ";
	}
	
	public void ResumePlay()
	{
		GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.ResumeGame();
	}

	// Load main menu if main menu button is pressed
	public void BackToMainMenu()
	{
		SceneManager.LoadScene("Main_Menu");
	}
}
