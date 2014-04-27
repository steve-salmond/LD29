using UnityEngine;
using System.Collections;

public class BottomLeft : MonoBehaviour {

	public Vector2 Offset;

	
	void LateUpdate () {
		transform.position = Camera.main.ScreenToWorldPoint(Offset);
	}
}
