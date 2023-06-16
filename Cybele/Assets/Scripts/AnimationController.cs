using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	public Rigidbody2D RB;
	private Animator anim;
	private enum MovementState { idle, running, jumping, falling }
	private Vector2 _moveInput;

	void Start()
    {
		RB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");

		MovementState state;

		if (Mathf.Abs(_moveInput.x) > 0f)
		{
			state = MovementState.running;
		}
		else
			state = MovementState.idle;

		if (RB.velocity.y > .1f)
		{
			state = MovementState.jumping;
		}
		else if (RB.velocity.y < -.1f)
		{
			state = MovementState.falling;
		}

		anim.SetInteger("AnimCont", (int)state);
	}
}
