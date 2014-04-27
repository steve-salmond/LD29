using UnityEngine;
using System.Collections;

public class RandomClip : MonoBehaviour {

	public AudioClip[] Clips;

	// Use this for initialization
	void Start() {
		int index = Random.Range(0, Clips.Length);
		audio.PlayOneShot(Clips[index]);
	}
	
}
