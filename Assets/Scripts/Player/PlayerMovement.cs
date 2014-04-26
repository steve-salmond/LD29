using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	/** Strength of left/right motion. */
	public float HorizontalStrength = 10;

	/** Horizontal speed limit. */
	public float HorizontalSpeedLimit = 10;

	/** Strength of up/down motion. */
	public float VerticalStrength = 10;

	/** Vertical speed limit. */
	public float VerticalSpeedLimit = 10;

	/** Strength of jump motion. */
	public float JumpStrength = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Get player's inputs.
		float ix = Input.GetAxis("Horizontal");
		float iy = Input.GetAxis("Vertical");
		float jump = Input.GetButtonDown("Jump") ? JumpStrength : 0;

		// Convert into desired force components.
		float dx = ix * HorizontalStrength;
		float dy = iy * VerticalStrength + jump;

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
