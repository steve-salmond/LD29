using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour 
{

	public int HitPoints = 3;

	public GameObject HitEffect;
	public GameObject DieEffect;
	public LayerMask HitterLayers;

	private int hits = 0;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Check if the object we've collided with can hit us.
		if ((HitterLayers.value & 1 << collision.gameObject.layer) != 0)
			Hit(collision);
	}
	
	public void Hit(Collision2D collision)
	{
		hits++;

		Vector3 p = transform.position;
		if (collision != null)
			p = collision.contacts[0].point;

		if (hits < HitPoints && HitEffect)
			Instantiate(HitEffect, p, transform.rotation);
		else if (hits == HitPoints && DieEffect)
		{
			Instantiate(DieEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	}
