// Handle animations of the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private Animator _anim;
	private Player _player;

	// Use this for initialization
	void Start ()
	{
		_anim = GetComponent<Animator>();
		_player = GetComponent<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		// Move engines according to player one's movements
		if (_player.isPlayerOne)
		{
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				_anim.SetBool("Turn_Left", true);
				_anim.SetBool("Turn_Right", false);
			}
			else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
			{
				_anim.SetBool("Turn_Left", false);
				_anim.SetBool("Turn_Right", false);
			}

			if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				_anim.SetBool("Turn_Left", false);
				_anim.SetBool("Turn_Right", true);
			}
			else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
			{
				_anim.SetBool("Turn_Left", false);
				_anim.SetBool("Turn_Right", false);
			}
		}

		// Move engines according to player two's movements
		else
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				_anim.SetBool("Turn_Left", true);
				_anim.SetBool("Turn_Right", false);
			}
			else if (Input.GetKeyUp(KeyCode.F))
			{
				_anim.SetBool("Turn_Left", false);
				_anim.SetBool("Turn_Right", false);
			}
			
			if (Input.GetKeyDown(KeyCode.H))
			{
				_anim.SetBool("Turn_Left", false);
				_anim.SetBool("Turn_Right", true);
			}
			else if (Input.GetKeyUp(KeyCode.H))
			{
				_anim.SetBool("Turn_Left", false);
				_anim.SetBool("Turn_Right", false);
			}
		}
	}
}