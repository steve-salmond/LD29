using UnityEngine;
using System.Collections;

public class Severable : MonoBehaviour 
{

	public LayerMask HurterLayers;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((HurterLayers.value & 1 << collision.gameObject.layer) != 0)
			Sever();
	}

	private void Sever()
	{
		Joint2D joint = GetComponent<Joint2D>();
		if (joint)
			joint.enabled = false;

		transform.parent = null;
	}
}
