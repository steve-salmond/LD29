using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Attraction Attraction;

	// Use this for initialization
	void Start () {
		Attraction.Target = Player.Instance.transform;
	}

//	void Update()
//	{
//		if (transform.position.x < Player.Instance.transform.position.x)
//			transform.localScale = new Vector3(-1, 1, 1);
//		else
//			transform.localScale = new Vector3(1, 1, 1);
//	}

}
