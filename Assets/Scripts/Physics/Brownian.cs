using UnityEngine;
using System.Collections;

public class Brownian : MonoBehaviour {

	/** Rigidbody that will be influenced. */
	public Rigidbody2D Body;
	
	/** Strength of the Brownian motion force. */
	public float Strength;

	void Start()
	{
		if (!Body)
			Body = rigidbody2D;
	}

	void FixedUpdate() 
	{
		Vector2 p = transform.position;
		Vector2 f = Random.insideUnitCircle * Strength;

		Body.AddForceAtPosition(f, p);
	}
}
