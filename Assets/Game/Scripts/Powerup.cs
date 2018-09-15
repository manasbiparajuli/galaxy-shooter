// Handle powerup behavior

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
	[SerializeField] private float _speed = 3.0f;

	// tripleshot = 0, speed boost =1, shield = 2
	[SerializeField] private int _powerupID; 

	[SerializeField] private AudioClip _clip;

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -7)
		{
			Destroy(this.gameObject);
		}
	}

	// Handle powerup behavior when the player receives powerup
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			// Access the Player GameObject
			Player player = other.GetComponent<Player>();
			// Play powerup sound when the player receives powerup
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

			if (player != null)
			{
				// enable triple shot
				if (_powerupID == 0)
				{
					player.TripleShotPowerupOn();
				}
				// enable speed boost here
				else if (_powerupID == 1)
				{
					player.SpeedPowerUpOn();
				}
				// enable shield
				else if (_powerupID == 2)
				{
					player.EnableShields();
				}
			}
			// Destroy ourselves
			Destroy(this.gameObject);
		}
	}
}
