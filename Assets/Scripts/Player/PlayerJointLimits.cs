using UnityEngine;
using System.Collections;

public class PlayerJointLimits : MonoBehaviour 
{

    public Vector2 FacingLeftLimits;
    public Vector2 FacingRightLimits;

    private HingeJoint2D _hingeJoint;
    private Facing _facing = Facing.Left;

    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint2D>();
    }

	void Update () 
    {
        if (Player.Instance.Facing != _facing)
            SetFacing(Player.Instance.Facing);
    }

    private void SetFacing(Facing facing)
    {
        _facing = facing;

        JointAngleLimits2D limits = new JointAngleLimits2D();

        switch (facing)
        {
            case Facing.Left:
                limits.min = FacingLeftLimits.x;
                limits.max = FacingLeftLimits.y; 
                break;
            case Facing.Right:
                limits.min = FacingRightLimits.x;
                limits.max = FacingRightLimits.y; 
                break;
        }

        _hingeJoint.limits = limits;
	
	}
}
