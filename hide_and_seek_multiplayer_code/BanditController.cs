using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour {

	public float walkSpeed;
	public Animator animator;
	public AudioSource footstepSource;
	public AudioClip[] footstepClips;
	public AudioSource backgroundMusic;
	private Vector3 movement;
	private float movementSqrMagnitude;
	private int step;

	// Update is called once per frame
	void Update()
	{
		getMovementVector();
		characterRotation();
		walkingAnimation();
		footstepAudio();
	}

	// Gets the input axis and clamps it
	void getMovementVector()
	{
		movement.x = Input.GetAxis("Horizontal");
		movement.z = Input.GetAxis("Vertical");
		Vector3.ClampMagnitude(movement, 1.0f);
		movementSqrMagnitude = Vector3.SqrMagnitude(movement);
	}


	// Moves character
	void characterPosition()
	{
		this.transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
	}

	// Rotates character
	void characterRotation()
	{
		if (Input.GetKey(KeyCode.A))
		{
            transform.Rotate(new Vector3(0, -150 * Time.deltaTime, 0));
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(new Vector3(0, 150 * Time.deltaTime, 0));
		}

	}

	// Sets movement animation for character
	void walkingAnimation()
	{
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("Run", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Run", false);
        }

        transform.Translate(movement.z * Vector3.forward * Time.deltaTime * 10);
	}

	// Swaps foot step audio for player as each audioclip is played
	void footstepAudio()
	{
		if (!footstepSource.isPlaying)
		{
			backgroundMusic.volume = 1.0f;
		}
		if (movementSqrMagnitude > 0.3f && !footstepSource.isPlaying)
		{
			if (step == 0)
			{
				step = 1;
			}
			else
			{
				step = 0;
			}
			footstepSource.clip = footstepClips[step];
			footstepSource.volume = movementSqrMagnitude;
			footstepSource.Play();
			backgroundMusic.volume = 0.5f;
		}
	}
}
