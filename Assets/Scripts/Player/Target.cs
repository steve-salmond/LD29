using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{

	public Transform FacingLeftIdle;
    public Transform FacingLeftActive;
    public Transform FacingRightIdle;
    public Transform FacingRightActive;

    public Transform Idle
    { get { return FacingRight ? FacingRightIdle : FacingLeftIdle;  } }

    public Transform Active
    { get { return FacingRight ? FacingRightActive : FacingLeftActive; } }

    public bool FacingRight
    { get { return Player.Instance.Facing == Facing.Right; } }

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
                if (_current != null && _current == Active)
                {
                    _current = Idle;
                    _nextTransition = Time.time + IdleInterval;
                }
                else
                {
                    _nextTransition = Time.time + ActiveInterval;
                    _current = Active;
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
