using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject Template;
	public int Limit = 0;
	public float IntervalMin = 1;
	public float IntervalMax = 5;

	private int count = 0;

	// Use this for initialization
	void Start () {
		ScheduleSpawn();
	}
	
	private void ScheduleSpawn()
	{
		float interval = Random.Range(IntervalMin, IntervalMax);
		Invoke("Spawn", interval);
	}

	private void Spawn()
	{
		Instantiate(Template, transform.position, transform.rotation);
		count++;

		if (Limit <= 0 || count < Limit)
			ScheduleSpawn();
	}
}
