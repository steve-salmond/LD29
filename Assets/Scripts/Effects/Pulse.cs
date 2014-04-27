using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	private Color color;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		color = spriteRenderer.color;
	}
	

	void Update () {
		float a = 0.7f + Mathf.Sin(Time.time * 2) * 0.3f;
		spriteRenderer.color = new Color(color.r, color.g, color.b, a);
	}
}
