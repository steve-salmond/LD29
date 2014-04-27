using UnityEngine;
using System.Collections;

public class BerserkPickup : MonoBehaviour {

	public GameObject PickupEffect;
	public LayerMask PickerUpperLayerMask;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (Player.Instance.BerserkEnergy >= 3)
			return;

		if ((PickerUpperLayerMask.value & 1 << collision.gameObject.layer) != 0)
			Pickup();
	}


	private void Pickup()
	{
		Player.Instance.BerserkEnergy++;
		Instantiate(PickupEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}

}
