using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	/** The object to follow. */
	public Transform Target;

	/** Smoothing time. */
	public float SmoothTime = 1;

	/** Offset to maintain from target. */
	public Vector3 Offset;

	/** Current velocity (used during smoothing). */
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		Vector3 p = Target.position + Offset;
		transform.position = p;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!Target)
			return;

		Vector3 p = Target.position + Offset;
		transform.position = Vector3.SmoothDamp(transform.position, p, ref velocity, SmoothTime); 
	}
}
