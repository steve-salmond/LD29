using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float BerserkDuration = 3;
	public float BerserkCooldown = 5;

	public static Player Instance
		{ get; private set; }

	public bool Dead
		{ get; private set; }

	public bool Beserk
		{ get; private set; }

	private Severable severable;
	private bool canBeserk = true;

	public delegate void BeserkEventHandler();
	public event BeserkEventHandler OnBeserkStart;
	public event BeserkEventHandler OnBeserkStop;

	// Use this for initialization
	void Awake() {
		Instance = this;
		severable = GetComponent<Severable>();

	}


	void Update()
	{

		if (severable.Severed && !Dead)
			Kill();

		if (Input.GetMouseButtonDown(0) && !Dead)
			if (!Beserk && canBeserk)
				StartCoroutine(GoBeserk());
	}

	private IEnumerator GoBeserk()
	{
		Beserk = true;
		canBeserk = false;

		if (OnBeserkStart != null)
			OnBeserkStart();

		yield return new WaitForSeconds(BerserkDuration);
		Beserk = false;

		if (OnBeserkStop != null)
			OnBeserkStop();

		yield return new WaitForSeconds(BerserkCooldown);
		canBeserk = true;
	}
	
	private void Kill()
	{
		Dead = true;
		Invoke("Restart", 5);
	}

	private void Restart()
	{
		Application.LoadLevel(0);
	}

}
