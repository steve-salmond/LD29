using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public static Fader Instance
		{ get; private set; }

	private SpriteRenderer spriteRenderer;
	
	private Color color;


	void Awake() {
		Instance = this;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
		color = spriteRenderer.color;
	}

	public void FadeIn(float duration = 5)
	{ 
		StopAllCoroutines();
		StartCoroutine(UpdateFadeIn(duration));
	}

	public void FadeOut(float duration = 5)
	{ 
		StopAllCoroutines();
		StartCoroutine(UpdateFadeOut(duration));
	}


	private IEnumerator UpdateFadeOut(float duration)
	{
		spriteRenderer.enabled = true;
		float start = Time.time;
		float end = start + duration;
		while (Time.time <= end)
		{
			color.a = (Time.time - start) / duration; 
			spriteRenderer.color = new Color(color.r, color.g, color.b, color.a);
			yield return new WaitForEndOfFrame();
		}
	}
	
	private IEnumerator UpdateFadeIn(float duration)
	{
		spriteRenderer.enabled = true;
		float start = Time.time;
		float end = start + duration;
		while (Time.time <= end)
		{
			color.a = (1 - (Time.time - start) / duration); 
			spriteRenderer.color = new Color(color.r, color.g, color.b, color.a);
			yield return new WaitForEndOfFrame();
		}
		spriteRenderer.enabled = false;
	}


}
