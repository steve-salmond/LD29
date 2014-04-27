using UnityEngine;
using System.Collections;

public class RandomPitch : MonoBehaviour {

	public float MinPitch;
	public float MaxPitch;


	void Start () {
		audio.pitch = Random.Range(MinPitch, MaxPitch);
	}

}
