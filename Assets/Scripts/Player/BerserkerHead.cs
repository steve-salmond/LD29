using UnityEngine;
using System.Collections;

public class BerserkerHead : MonoBehaviour {

	public SpriteRenderer NormalHead;
	public GameObject BerserkHead;

	
	public void Start()
	{
		Player.Instance.OnBerserkStart += HandleOnBeserkStart;
		Player.Instance.OnBerserkStop += HandleOnBeserkStop;
	}
	
	void HandleOnBeserkStart ()
	{
		NormalHead.enabled = false;
		BerserkHead.SetActive(true);
	}
	
	void HandleOnBeserkStop ()
	{
		NormalHead.enabled = true;
		BerserkHead.SetActive(false);
	}
}
