using UnityEngine;
using System.Collections;

public class BerserkerMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Player.Instance.Berserk)
		{
			// audio.volume = 1;
			audio.pitch  = 2;
		}
		else
		{
			// audio.volume = 0.5f;
			audio.pitch  = 0.5f;
		}
	}
}
