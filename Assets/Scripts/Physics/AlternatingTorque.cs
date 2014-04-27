using UnityEngine;
using System.Collections;

public class AlternatingTorque : MonoBehaviour {

	public float Strength;

	public float Interval = 1;

	private float torque;

	private Severable severable;

	// Use this for initialization
	void Start () {
		torque = Strength;
		severable = GetComponent<Severable>();
		InvokeRepeating("SwitchTorque", 0, Interval);
	}

	void SwitchTorque()
	{
		torque = -torque;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!severable.Severed)
			rigidbody2D.AddTorque(torque);
	}
}
