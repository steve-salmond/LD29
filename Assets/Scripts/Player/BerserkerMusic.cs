using UnityEngine;
using System.Collections;

public class BerserkerMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Player.Instance.Beserk)
			audio.volume = 1;
		else
			audio.volume = 0.25f;
	}
}
