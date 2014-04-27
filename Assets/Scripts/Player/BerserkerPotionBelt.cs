using UnityEngine;
using System.Collections;

public class BerserkerPotionBelt : MonoBehaviour {

	private SpriteRenderer spriteRenderer;


	public int EnergyThreshold = 1;
	public GameObject Flare;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		spriteRenderer.enabled = Player.Instance.BerserkEnergy >= EnergyThreshold;
		Flare.particleSystem.enableEmission = spriteRenderer.enabled;
	}
}
