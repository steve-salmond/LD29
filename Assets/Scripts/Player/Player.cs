using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public static Player Instance
		{ get; private set; }

	// Use this for initialization
	void Awake() {
		Instance = this;
	}

}
