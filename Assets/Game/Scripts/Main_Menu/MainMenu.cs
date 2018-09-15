// Load Coop or single player game based on player input

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadSinglePlayerGame()
	{
		SceneManager.LoadScene("Single_Player");
	}

	public void LoadCoOpPlayerGame()
	{
		SceneManager.LoadScene("Co-Op_Mode");
	}
}
