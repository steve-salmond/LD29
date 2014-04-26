using UnityEngine;
using System.Collections;

public class BerserkerFlail : MonoBehaviour {

	public Rigidbody2D Shoulder;

	public Attraction Control;

	public float Strength = 20;


	public void Start()
	{
		Player.Instance.OnBeserkStart += HandleOnBeserkStart;
		Player.Instance.OnBeserkStop += HandleOnBeserkStop;
	}

	void HandleOnBeserkStart ()
	{
		Shoulder.AddTorque(Strength);
		Control.Active = false;
	}

	void HandleOnBeserkStop ()
	{
		Control.Active = true;
	}


}
