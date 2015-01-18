using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{

	public Transform Idle;
    public Transform Active;
    public string Button;

	public Transform Hero;

    private float ActiveInterval = 0.3f;
    private float IdleInterval = 0.5f;

    private Transform _current;
    private float _nextTransition;

	void FixedUpdate () 
    {

        if (Input.GetAxis(Button) > 0)
        {
            if (Time.time > _nextTransition)
            {
                if (_current == null || _current == Idle)
                {
                    _nextTransition = Time.time + ActiveInterval;
                    _current = Active;
                }
                else if (_current == Active)
                {
                    _current = Idle;
                    _nextTransition = Time.time + IdleInterval;
                }
            }

            transform.position = _current.position;
        }
        else
        {
            transform.position = Idle.position;
            _current = null;
            _nextTransition = 0;
        }
    }


}
