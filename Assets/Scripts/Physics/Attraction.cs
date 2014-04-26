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


	void FixedUpdate () 
	{
		Vector2 t = Target.position;
		Vector2 p = transform.position;
		Vector2 d = (t - p);
		Vector2 f = d * Mathf.Clamp(d.magnitude * Strength, MinForce, MaxForce);

		Body.AddForceAtPosition(f, p);
	}
}
