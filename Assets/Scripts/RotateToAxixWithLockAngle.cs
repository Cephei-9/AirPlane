using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAxixWithLockAngle : RotateToAxis
{
    public float maxAngle = 40;

    private float angle;

    public float RotateWithControlAngle(float angle)
    {
        if (isWork == false) return 0;

        this.angle = angle;
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        float rotation = Rotate();
        float angleWithNewRotate = angle + rotation;
        if (Mathf.Abs(angleWithNewRotate) > maxAngle)
        {
            if (Mathf.RoundToInt(Mathf.Sign(reserv)) == Mathf.RoundToInt(signRotate))
            {
                isWork = false;
                print("Work false Controle angle");
                print("SignRotate " + signRotate + ", reserv " + reserv + ", Angle " + angle + " Rotation " + rotation);
            }
            reserv = 0;
            return 0;
        }
        return rotation;
    }

    public override void InputControle(float nowAxisRow)
    {        
        if (Mathf.Abs(oldAxisRow - nowAxisRow) == 1)
        {
            if (Mathf.Abs(oldAxisRow) == 0)
            {
                isInput = true;
                isWork = true;
                print("begin rotate");
            }
            else
            {
                isInput = false;
                multiply = 1;
                print("end rotate");
            }
        }
        else if (Mathf.Abs(oldAxisRow - nowAxisRow) == 2)
        {
            if (nowAxisRow == 1)
            { multiply = 1; }

            multiply = 1;
            isWork = true;
            print("Change rotate");
        }
        signRotate = nowAxisRow;
        oldAxisRow = nowAxisRow;
    }

    protected override float Multiply()
    {
        if (Mathf.Sign(angle) == signRotate)
        {
            return Mathf.InverseLerp(40, 30, Mathf.Abs(angle));
        }
        return 1;
    }
}
