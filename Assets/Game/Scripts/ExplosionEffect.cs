// Handle explosion effects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{ 
	// Use this for initialization
	void Start ()
	{
		// Destroy the powerup after the animation ends 
		// Since our animation time is around 3f seconds, we set our objects to
		// get destroyed after 4 seconds
		Destroy(this.gameObject, 4f);
	}
}
