using UnityEngine;
using System.Collections;

public class BerserkerFlail : MonoBehaviour {

	public Rigidbody2D Shoulder;

	public Attraction Control;

	public float Strength = 20;


	void Update()
	{
		if (Player.Instance.Berserk)
		{
			Shoulder.AddTorque(Strength);
			Control.Active = false;
		}
		else
			Control.Active = true;
	}


}
