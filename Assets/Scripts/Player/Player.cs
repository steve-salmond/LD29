using UnityEngine;
using System.Collections;

public enum Facing { Left, Right };

public class Player : MonoBehaviour 
{

	public float BerserkDuration = 3;
	public float BerserkCooldown = 5;

	public GameObject BerserkEffect;

	public static Player Instance
		{ get; private set; }

	public bool Dead
		{ get; private set; }

	public bool Berserk
		{ get; private set; }

    public Facing Facing
    { get; private set; }
    
	public int BerserkEnergy = 0;

	private Severable severable;
	private bool cool = true;

	public delegate void BerserkEventHandler();
	public event BerserkEventHandler OnBerserkStart;
	public event BerserkEventHandler OnBerserkStop;

	// Use this for initialization
	void Awake() {
		
        Instance = this;
        Facing = Facing.Right;
		severable = GetComponent<Severable>();
		Fader.Instance.FadeIn();
	}


	void Update()
	{

		if (severable.Severed && !Dead)
			Kill();

        if (Input.GetButton("Fire3"))
            Berserk = true;
        else
            Berserk = false;

        if (Input.GetAxis("Horizontal") > 0)
            Facing = Facing.Right;
        else if (Input.GetAxis("Horizontal") < 0)
            Facing = Facing.Left;

        /*
		if (Input.GetButtonDown("Fire1"))
			if (CanBerserk())
				StartCoroutine(GoBerserk());
        */

		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}

	private bool CanBerserk()
	{
		return !Berserk && cool && !Dead && BerserkEnergy > 0;
	}

	private IEnumerator GoBerserk()
	{
		Berserk = true;
		cool = false;
		BerserkEnergy--;

		if (OnBerserkStart != null)
			OnBerserkStart();

		if (BerserkEffect)
		{
			GameObject go = Instantiate(BerserkEffect) as GameObject;
			go.transform.parent = transform;
			go.transform.position = transform.position;
			go.transform.rotation = transform.rotation;
		}

		yield return new WaitForSeconds(BerserkDuration);
		Berserk = false;

		if (OnBerserkStop != null)
			OnBerserkStop();

		yield return new WaitForSeconds(BerserkCooldown);
		cool = true;
	}
	
	private void Kill()
	{
		Dead = true;
		Fader.Instance.FadeOut();
		Invoke("Restart", 5.1f);
	}

	private void Restart()
	{
		Application.LoadLevel(0);
	}

}
