// Handle player behavior

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	// Prefabs and engines to manipulate
	[SerializeField] private GameObject _laserPrefab;
	[SerializeField] private GameObject _tripleShotPrefab;
	[SerializeField] private GameObject _explosionPrefab;
	[SerializeField] private GameObject _shieldGameObject;
	[SerializeField] private GameObject[] _engines;

	private int hitCount = 0;
	// canFire -- has the amount of time between firing passed?
	private float _canFire = 0.0f;
	[SerializeField] private int _lives = 3;
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _powerupSpeed = 1.5f;
	[SerializeField] private float _fireRate = 0.1f;

	public bool canTripleShot = false;
	public bool hasSpeedPowerup = false;
	public bool shieldsActive = false;
	public bool isPlayerOne = false;
	public bool isPlayerTwo = false;

	private UIManager _uiManager;
	private GameManager _gameManager;
	private AudioSource _audioSource;

	private void Start()
	{
		// Update the UI for the player's lives at the start of the game
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

		// Proceed only if we don't have a null reference
		if (_uiManager != null)
		{
			_uiManager.UpdateLives(_lives);
		}

		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		// reset the player's position to zero only in single player mode
		if (_gameManager.isCoopMode == false)
		{
			// current pos = new pos
			transform.position = new Vector3(0, 0, 0);
		}

		_audioSource = GetComponent<AudioSource>();

		hitCount = 0;
	}

	// Update is called once per frame
	private void Update()
	{
		if (isPlayerOne == true)
		{
			Movement();
			// if space key is pressed, spawn laser at player position

			// Handle joystick movement in android for player 1
#if UNITY_ANDROID
			if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && isPlayerOne == true)
			{
				Shoot();
			}

#else
			if (Input.GetKeyDown(KeyCode.Space) && isPlayerOne == true)
			{
				Shoot();
			}
#endif
		}

		// Player two behavior
		if (isPlayerTwo == true)
		{
			Player2Movement();
			if (Input.GetKeyDown(KeyCode.Tab) && isPlayerTwo == true)
			{
				Shoot();
			}
		}
	}

	private void Shoot()
	{
		// Specify some time before a player can fire the next laser
		if (Time.time > _canFire)
		{
			// Play laser shooting sound when the laser is shot
			_audioSource.Play();

			// Check if the user has the ability to triple shot
			if (canTripleShot)
			{
				Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
			}
			else
			{
				// spawn the laser
				// Quaternion.identity => no rotation
				// new Vector3(0,0.88f, 0) => fire laser beam in y-coordinate plane above the player's battleship
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
			}
			_canFire = Time.time + _fireRate;
		}
	}

	private void Movement()
	{
		// move the object horizontally
		float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
		float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

		// Check if the user has the speed powerup
		// If player does, then increase the speed by powerupSpeed
		// move the player1 horizontally using left, right key
		// move the player1 vertically using up, down key
		if (hasSpeedPowerup)
		{
			transform.Translate(translation: Vector3.right * _speed * _powerupSpeed * horizontalInput * Time.deltaTime);
			transform.Translate(translation: Vector3.up * _speed * _powerupSpeed * verticalInput * Time.deltaTime);
		}
		else
		{
			transform.Translate(translation: Vector3.right * _speed * horizontalInput * Time.deltaTime);
			transform.Translate(translation: Vector3.up * _speed * verticalInput * Time.deltaTime);
		}

		// if player on the y axis > 0
		// set player position on the Y to 0
		if (transform.position.y > 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y < -4.2f)
		{
			transform.position = new Vector3(transform.position.x, -4.2f, 0);
		}

		// Wrap the scene
		if (transform.position.x > 9.5f)
		{
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.5f)
		{
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}
	}

	private void Player2Movement()
	{
		// Check if the user has the speed powerup
		// If player does, then increase the speed by powerupSpeed
		if (hasSpeedPowerup)
		{
			// Movement system for Player 2 : THFG
			if (Input.GetKey(KeyCode.T))
			{ 
				transform.Translate(Vector3.up * _speed * _powerupSpeed  * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.H))
			{
				transform.Translate(Vector3.right * _speed * _powerupSpeed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.F))
			{
				transform.Translate(Vector3.left * _speed * _powerupSpeed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.G))
			{
				transform.Translate(Vector3.down * _speed * _powerupSpeed * Time.deltaTime);
			}
		}
		else
		{
			// Movement system for Player 2 : THFG
			if (Input.GetKey(KeyCode.T))
			{
				transform.Translate(Vector3.up * _speed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.H))
			{
				transform.Translate(Vector3.right * _speed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.F))
			{
				transform.Translate(Vector3.left * _speed * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.G))
			{
				transform.Translate(Vector3.down * _speed * Time.deltaTime);
			}
		}

		// if player on the y axis > 0
		// set player position on the Y to 0
		if (transform.position.y > 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y < -4.2f)
		{
			transform.position = new Vector3(transform.position.x, -4.2f, 0);
		}

		// Wrap the scene
		if (transform.position.x > 9.5f)
		{
			transform.position = new Vector3(-9.5f, transform.position.y, 0);
		}
		else if (transform.position.x < -9.5f)
		{
			transform.position = new Vector3(9.5f, transform.position.y, 0);
		}
	}

	public void Damage()
	{
		// Ensure that tht player does not get any damage when shields are active
		if (shieldsActive)
		{
			shieldsActive = false;
			_shieldGameObject.SetActive(false);
			return;
		}

		hitCount++;

		// Based on hit count, turn the engine failure animation
		if (hitCount == 1)
		{
			// turn left engine failure on
			_engines[0].SetActive(true);
		}
		else if (hitCount == 2)
		{
			// turn right engine failure on
			_engines[1].SetActive(true);
		}

		// subtract 1 life from the player
		_lives--;
		_uiManager.UpdateLives(_lives);

		// if lives < 1, destroy the object
		if (_lives < 1)
		{
			// Update best score before ending the game
			_uiManager.CheckForBestScore();
			// Explode player and initiate animation when player dies
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

			// End the game and show the main title screen
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen();
			Destroy(this.gameObject);
		}
	}

	public void TripleShotPowerupOn()
	{
		canTripleShot = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	// Coroutine that will stop tripleshot ability after 5 seconds
	public IEnumerator TripleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		canTripleShot = false;
	}

	public void SpeedPowerUpOn()
	{
		hasSpeedPowerup = true;
		StartCoroutine(SpeedPowerDownRoutine());
	}

	// Coroutine that will stop speedpowerup after 5 seconds
	public IEnumerator SpeedPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		hasSpeedPowerup = false;
	}

	public void EnableShields()
	{
		shieldsActive = true;
		_shieldGameObject.SetActive(true);
	}
}