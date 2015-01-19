using UnityEngine;
using System.Collections;

public class MouseTarget: MonoBehaviour {

	public Transform Idle;

	public Transform Hero;

	private Vector3 screen;

	public Vector2 JoystickScale = Vector2.one;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float dx = Input.GetAxis("Sword Horizontal") * JoystickScale.x;
        float dy = Input.GetAxis("Sword Vertical") * JoystickScale.y;

        if (Input.GetMouseButton(1))
        {
            Vector3 p = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            dx = 2 * (p.x - 0.5f) * JoystickScale.x;
            dy = 2 * (0.5f - p.y) * JoystickScale.y;
            // transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        transform.position = Hero.position + new Vector3(dx, -dy, 0);

	}
}
