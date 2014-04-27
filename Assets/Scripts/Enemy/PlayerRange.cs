using UnityEngine;
using System.Collections;

public class PlayerRange : MonoBehaviour {

	public float Range;

	public GameObject Child;

	// Use this for initialization
	void Awake () {

		Child.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 p = Player.Instance.transform.position;
		Vector3 m = transform.position;

		if (Vector3.Distance(p, m) < Range)
			if (!Child.activeSelf)
			{
				Child.SetActive(true);
				this.enabled = false;
			}
	}
}
