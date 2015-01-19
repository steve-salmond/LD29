using UnityEngine;
using System.Collections;

public class PlayerFacing : MonoBehaviour 
{

    public Facing Facing;

    private SpriteRenderer[] _renderers;
    private bool _visible = true;

	void Start () 
    {
        _renderers = GetComponentsInChildren<SpriteRenderer>();
        UpdateFacing();
	}
	
	void LateUpdate()
    {
        UpdateFacing();
	}

    private void UpdateFacing()
    {
        bool visible = Facing == Player.Instance.Facing;
        if (visible != _visible)
            SetVisible(visible);
    }

    private void SetVisible(bool value)
    {
        _visible = value;

        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].enabled = value;
    }
}
