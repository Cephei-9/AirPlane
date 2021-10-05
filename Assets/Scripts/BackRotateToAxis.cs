using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRotateToAxis : RotateToAxis
{

    /*
     * Скрипт оболочка над RotateToAxis которая добавляет функционал обратного вращения. Это нужно что бы когда са
     */

    public float friction = 0.2f;

    private float angle;

    public float RotateBack()
    {
        if (isWork == false) return 0;

        float rotation = Rotate();
        AddFriction(rotation);

        return rotation;
    }

    private void AddFriction(float rotation)
    {
        float angleWithNewRotate = Mathf.Abs(angle) - Mathf.Abs(rotation);
        //print(angleWithNewRotate);
        if (angleWithNewRotate < 0)
        {
            reserv *= 0.8f;
            //print("BackFriction");
        }
        if (Mathf.Sign(reserv) != signRotate)
        {
            //print("AddFriction");
            reserv -= reserv * friction;
        }
    }

    public void InputControle(float nowAxisRow, float angle)
    {
        this.angle = angle;

        if (Mathf.Abs(oldAxisRow - nowAxisRow) == 1)
        {
            if (Mathf.Abs(oldAxisRow) == 1)
            {
                isInput = true;
                isWork = true;
            }
            else
            {
                isInput = false;
            }
        }
        signRotate = -Mathf.Sign(angle);
        oldAxisRow = nowAxisRow;
    }
}
