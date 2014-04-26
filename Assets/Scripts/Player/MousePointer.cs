using UnityEngine;
using System.Collections;

public class MousePointer : MonoBehaviour {

	private Vector3 screen;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
