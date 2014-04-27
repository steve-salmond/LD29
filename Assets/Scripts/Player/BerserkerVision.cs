using UnityEngine;
using System.Collections;

public class BerserkerVision : MonoBehaviour {


	private SpriteRenderer spriteRenderer;

	private Color color;

	public void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
		color = spriteRenderer.color;
		Player.Instance.OnBeserkStart += HandleOnBeserkStart;
		Player.Instance.OnBeserkStop += HandleOnBeserkStop;
	}
	
	void HandleOnBeserkStart ()
	{
		StartCoroutine(FadeIn());
	}
	
	void HandleOnBeserkStop ()
	{
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeIn()
	{
		spriteRenderer.enabled = true;
		float duration = 1;
		float start = Time.time;
		float end = start + duration;
		while (Time.time <= end)
		{
			color.a = (Time.time - start) / duration * 0.5f; 
			spriteRenderer.color = new Color(color.r, color.g, color.b, color.a);
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator FadeOut()
	{
		float duration = 1;
		float start = Time.time;
		float end = start + duration;
		while (Time.time <= end)
		{
			color.a = (1 - (Time.time - start) / duration) * 0.5f; 
			spriteRenderer.color = new Color(color.r, color.g, color.b, color.a);
			yield return new WaitForEndOfFrame();
		}
		spriteRenderer.enabled = false;
	}

}
