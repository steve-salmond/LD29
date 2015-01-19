using UnityEngine;
using System.Collections;

public class MatchTransform : MonoBehaviour {

    public Transform Target;

    private Transform _transform;

	void Start () {
        _transform = transform;
        MatchTargetTransform();
	}
	
	void Update () {
        MatchTargetTransform();
	}

    private void MatchTargetTransform()
    {
        if (Target == null)
            return;

        _transform.position = Target.position;
        _transform.rotation = Target.rotation;
    }
}
