using UnityEngine;
using System.Collections;

public class Severable : MonoBehaviour 
{

	public GameObject SeverEffect;
	public LayerMask HurterLayers;

	private bool severed = false;


	private SpriteRenderer[] spriteRenderers;
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((HurterLayers.value & 1 << collision.gameObject.layer) != 0)
			Sever(collision);
	}

	public void Sever(Collision2D collision)
	{
		if (severed)
			return;
	
		severed = true;

		Joint2D joint = GetComponent<Joint2D>();
		if (joint)
			joint.enabled = false;

		transform.parent = null;

		if (SeverEffect)
		{

			GameObject go = Instantiate(SeverEffect, collision.contacts[0].point, transform.rotation) as GameObject;
			go.transform.parent = transform;
		}

		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		StartCoroutine(Kill());
	}

	private IEnumerator Kill()
	{
		yield return new WaitForSeconds(3);

		float start = Time.time;
		float end = start + 0.5f;

		while (Time.time < end)
		{
			float a = 1 - Mathf.Clamp01((Time.time - start));
			foreach (SpriteRenderer r in spriteRenderers)
				if (r)
					r.color = new Color(1, 1, 1, a);

			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
	}
}
