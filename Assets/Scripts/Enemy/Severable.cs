using UnityEngine;
using System.Collections;

public class Severable : MonoBehaviour 
{

	public GameObject SeverEffect;
	public LayerMask HurterLayers;
	public bool SeverChildren = false;
	private bool severed = false;


	private SpriteRenderer[] spriteRenderers;
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((HurterLayers.value & 1 << collision.gameObject.layer) != 0)
			Sever(collision);
	}

	public void Sever(Collision2D collision = null)
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
			Vector3 p = transform.position;
			if (collision != null)
				p = collision.contacts[0].point;

			GameObject go = Instantiate(SeverEffect, p, transform.rotation) as GameObject;
			go.transform.parent = transform;
		}

		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		StartCoroutine(Kill());

		if (SeverChildren)
		{
			Severable[] severables = GetComponentsInChildren<Severable>();
			foreach (Severable s in severables)
				s.Sever();
		}
	}

	private IEnumerator Kill()
	{
		Attraction attraction = GetComponent<Attraction>();
		if (attraction)
			attraction.enabled = false;

		yield return new WaitForSeconds(3);

		float start = Time.time;
		float end = start + 0.5f;

		while (Time.time < end)
		{
			float a = 1 - Mathf.Clamp01((Time.time - start));
			foreach (SpriteRenderer r in spriteRenderers)
				if (r)
					r.color = new Color(1, 1, 1, Mathf.Min(a, r.color.a));

			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
	}
}
