using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public static Player Instance
		{ get; private set; }

	public bool Dead
		{ get; private set; }

	private Severable severable;

	// Use this for initialization
	void Awake() {
		Instance = this;
		severable = GetComponent<Severable>();
	}


	void Update()
	{

		if (severable.Severed && !Dead)
			Kill();
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
