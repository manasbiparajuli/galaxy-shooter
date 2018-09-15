// Handle EnemyAI behavior

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private GameObject _enemyExplosionPrefab;

	// enemy speed
	private float _speed = 5.0f;

	private UIManager _uiManager;
	[SerializeField] private AudioClip _clip;

	// Use this for initialization
	void Start ()
	{
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// move down
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		// when off the screen on the bottom
		// respawn back on top with a new random x position between the bounds of the screen
		if (transform.position.y < -7)
		{
			float randomX = Random.Range(-7f, 7f);
			transform.position = new Vector3(randomX, 7, 0);
		}
	}

	// Handle behavior when the enemy gets hit by the laser or the player
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Enemy hit by the laser
		if (other.tag == "Laser")
		{
			if (other.transform.parent != null)
			{
				Destroy(other.transform.parent.gameObject);
			}
			Destroy(other.gameObject);

			// Animate enemy explosion at current position
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

			// Update Score only if the laser hits the enemy
			_uiManager.UpdateScore();
			// Play getting destroyed sound and destroy the enemy object
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
			Destroy(this.gameObject);
		}

		// Enemy and player clashed
		else if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			// Player has been damaged
			if (player != null)
			{
				player.Damage();
			}

			// Start enemy explosion animation and play audio
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
			Destroy(this.gameObject);
		}
	}
}