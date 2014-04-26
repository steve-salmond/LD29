using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {

	public string Layer;
	
	void Start () {
		renderer.sortingLayerName = Layer;
	}

}
