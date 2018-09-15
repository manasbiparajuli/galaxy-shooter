// Handle Laser Behavior

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	// Speed of the laser
	public float _speed = 10.0f;

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.up * _speed * Time.deltaTime);

		// destroy laser once it goes out of view in the scene
		// if position on the Y axis of the laser if greater than or equal to 6
		// destroy the laser
		if (transform.position.y >= 6)
		{
			// destroy the parent of the laser
			if (transform.parent != null)
			{
				Destroy(transform.parent.gameObject);
			}
			Destroy(this.gameObject);   
		}
	}
}
