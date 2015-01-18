using UnityEngine;
using System.Collections;

public class Severable : MonoBehaviour 
{

	public GameObject SeverEffect;
	public GameObject SpurtEffect;
	public LayerMask HurterLayers;

    public float Health = 1;

	public bool SeverChildren = false;
	public bool DestroyAfterSever = true;
	public bool Severed
		{ get { return severed; } }

	private int SeveredLayer = 11;
	
	private bool severed = false;


	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Check if the object we've collided with can sever us.
		if ((HurterLayers.value & 1 << collision.gameObject.layer) != 0)
		{
//			// Ignore collisions with severed objects.
//			Severable severable = collision.gameObject.GetComponent<Severable>();
//			if (severable && severable.severed)
//				return;

            // Reduce object's health according to collision strength.
            float damage = GetCollisionDamage(collision);
            Health = Mathf.Clamp01(Health - damage);

			// Sever away, good sir!
            if (Health == 0 && !severed)
			    Sever(collision);
		}
	}

    private float GetCollisionDamage(Collision2D collision)
    {
        float magnitude = collision.relativeVelocity.magnitude;
        // Debug.Log("Magnitude: " + magnitude);
        return Mathf.Clamp01(magnitude / 200.0f);
    }

	public void Sever(Collision2D collision = null)
	{
		// Check if object has already been severed. 
		if (severed)
			return;
	
		severed = true;

		// Spawn severing effects.
		if (SpurtEffect && transform.parent)
		{
			GameObject go = Instantiate(SpurtEffect) as GameObject;
			go.transform.parent = transform.parent;
			go.transform.position = transform.position;
			go.transform.rotation = transform.parent.rotation;
		}

		if (SeverEffect)
		{
			Vector3 p = transform.position;
			if (collision != null)
				p = collision.contacts[0].point;

			GameObject go = Instantiate(SeverEffect, p, transform.rotation) as GameObject;
			go.transform.parent = transform;
		}

		// Disable any joints that attach object to its parent.
		Joint2D[] joints = GetComponentsInChildren<Joint2D>();
		foreach (Joint2D joint in joints)
			joint.enabled = false;

		// Detach object from parent.
		transform.parent = null;

		// Allow object to move freely.
		rigidbody2D.fixedAngle = false;

		// Move all rigidbodies to the severed layer.
		rigidbody2D.gameObject.layer = SeveredLayer;
		Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D body in bodies)
			body.gameObject.layer = SeveredLayer;

		// Stop any physics forces being applied to the object.
		Attraction[] attractions = GetComponentsInChildren<Attraction>();
		foreach (Attraction attraction in attractions)
			attraction.enabled = false;

		// Disable player movement if needed.
		PlayerMovement movement = GetComponent<PlayerMovement>();
		if (movement)
			movement.enabled = false;

		// Fade out and destroy object after some time.
		if (DestroyAfterSever)
			StartCoroutine(Kill());

		// Sever all children (if desired).
		if (SeverChildren)
		{
			Severable[] severables = GetComponentsInChildren<Severable>();
			foreach (Severable s in severables)
				s.Sever();
		}
	}

	/** Disposes of the object after a delay. */
	private IEnumerator Kill()
	{
		float start = Time.time;
		float end = start + 1;
		AudioSource[] audios = GetComponentsInChildren<AudioSource>();
		while (Time.time < end)
		{
			float a = 1 - Mathf.Clamp01((Time.time - start));
			foreach (AudioSource source in audios)
				if (source)
					source.volume = Mathf.Min(a, source.volume);
			
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(2);

		start = Time.time;
		end = start + 1;

		ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem ps in particleSystems)
			ps.enableEmission = false;

		SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		while (Time.time < end)
		{
			float a = 1 - Mathf.Clamp01((Time.time - start));
			foreach (SpriteRenderer r in spriteRenderers)
				if (r)
					r.color = new Color(1, 1, 1, Mathf.Min(a, r.color.a));
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
