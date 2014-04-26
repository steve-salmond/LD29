using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float Delay = 2;

	void Start () {
		Invoke("Kill", Delay);
	}
	 
	private void Kill()
		{ Destroy(gameObject); }
}
