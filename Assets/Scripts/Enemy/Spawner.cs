using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject Template;
	public int Limit = 0;
	public float IntervalMin = 1;
	public float IntervalMax = 5;

	public bool Triggered = true;
	public Vector3 Offset;

	private int count = 0;
	private bool spawning = false;

	// Use this for initialization
	void Start () {
		if (!Triggered)
			Spawn();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!spawning)
			if (Triggered && other.tag == "Player")
				Spawn();
	}

	private void ScheduleSpawn()
	{
		spawning = true;
		float interval = Random.Range(IntervalMin, IntervalMax);
		Invoke("Spawn", interval);
	}

	private void Spawn()
	{
		Instantiate(Template, transform.position + Offset, transform.rotation);
		count++;

		if (Limit <= 0 || count < Limit)
			ScheduleSpawn();
	}
}
