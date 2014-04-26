using UnityEngine;
using System.Collections;

public class Attraction : MonoBehaviour {

	/** Rigidbody that will be influenced by this attraction. */
	public Rigidbody2D Body;

	/** Target we're attracted to. */
	public Transform Target;

	/** Strength of the attraction. */
	public float Strength;

	/** Minimum attractive force to apply. */
	public float MinForce;

	/** Maximum attractive force to apply. */
	public float MaxForce;

	/** Maximum distance for attractive force. */
	public float MaxDistance;


	void FixedUpdate() 
	{
		Vector2 t = Target.position;
		Vector2 p = transform.position;
		Vector2 d = (t - p);

		float distance = Mathf.Clamp(d.magnitude, 0, MaxDistance);
		Vector2 f = d * Mathf.Clamp(distance * Strength, MinForce, MaxForce);

		Body.AddForceAtPosition(f, p);
	}
}
