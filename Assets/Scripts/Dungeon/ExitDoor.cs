using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			StartCoroutine(Exit());
	}

	IEnumerator Exit()
	{
		Fader.Instance.FadeOut();
		yield return new WaitForSeconds(5.1f);
		Application.LoadLevel(0);
	}
}
