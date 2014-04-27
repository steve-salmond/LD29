using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	/** Strength of left/right motion. */
	public float HorizontalStrength = 10;

	/** Strength of left/right motion while in air. */
	public float HorizontalStrengthAirborne = 10;

	/** Horizontal speed limit. */
	public float HorizontalSpeedLimit = 10;

	/** Strength of up/down motion. */
	public float VerticalStrength = 10;

	/** Vertical speed limit. */
	public float VerticalSpeedLimit = 10;

	/** Strength of jump motion. */
	public float JumpStrength = 100;

	/** Properties that determine whether player is grounded. */
	public Vector2 GroundCheckOffset;
	public float GroundCheckRadius = 2;
	public LayerMask GroundLayerMask;

	/** Left leg. */
	public Severable LeftLeg;

	/** Right leg. */
	public Severable RightLeg;

	/** Number of jumps player can make while airborne. */
	public int AirJumps = 0;

	/** Jump sound. */
	public AudioClip JumpSound;
	
	/** Number of jumps player has made since last grounding. */
	private int jumps = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Get player's inputs.
		float ix = Input.GetAxis("Horizontal");
		float iy = Input.GetAxis("Vertical");
		float jump = Input.GetButtonDown("Jump") ? JumpStrength : 0;

		// If player is wanting to jump, check whether they are grounded.
		// If not, allow up to JumpCount jumps, then ignore further input.
		// Jump counter is reset when player hits ground again.
		Vector2 p = transform.position;
		bool grounded = Physics2D.OverlapCircle(p + GroundCheckOffset, GroundCheckRadius, GroundLayerMask) != null;
		if (grounded)
			{ jumps = 0; }
		else if (jump > 0 && jumps < AirJumps)
			{ jumps++; }
		else
			jump = 0;

		// Convert into desired force components.
		float dx = ix * (grounded ? HorizontalStrength : HorizontalStrengthAirborne);
		float dy = iy * VerticalStrength + jump;

		// Check if legs are severed.
		if (LeftLeg.Severed)
			{ dx *= 0.5f; dy *= 0.5f; }
		if (RightLeg.Severed)
			{ dx *= 0.5f; dy *= 0.5f; }

		if (jump > 0)
			audio.PlayOneShot(JumpSound);

		// Figure out if we need to reduce the force due to speed limits.
		Vector2 v = rigidbody2D.velocity;
		float sx = Mathf.Clamp01(1 - Mathf.Abs(v.x) / HorizontalSpeedLimit);
		float sy = Mathf.Clamp01(1 - Mathf.Abs(v.y) / VerticalSpeedLimit);

		// Only apply speed limiting if input is in same direction as velocity.
		if (v.x * dx > 0)
			dx *= sx;
		if (v.y * dy > 0)
			dy *= sy;

		// Add final input forces to player's rigid body. 
		rigidbody2D.AddForce(new Vector2(dx, dy));
	}
}
