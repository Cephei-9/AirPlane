using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAxis : MonoBehaviour
{

    /*
     * Главный скрипт вращения. Принимает инпут, сравнивает его с инпутом предыдущего вызова,и решает изменить направление вращения, нет
     * или остановить его. Метод ротэйт постоянно прибавляет к переменной резерв сонаправленное с инпутом вращение, это нужно для того 
     * чтобы обеспечить плавность и заносы
     */

    public string Name;

    public float speedRotate = 1;
    public float speedReserv = 0.02f;
    public float speedReservRevers = 0.02f;
    public float maxReserv = 1;

    public float Delta { get; private set; }

    public bool isInput = false;
    public bool isWork = false;

    protected float reserv = 0;
    protected float signRotate;
    protected float oldAxisRow;
    public float multiply = 1;

    public float lastRotation;

    public virtual float Rotate()
    {
        if (isWork == false) return 0;

        if (isInput)
        {
            float localSpeedReserv = speedReserv;
            if (signRotate != Mathf.Sign(reserv)) localSpeedReserv = speedReservRevers;

            reserv += signRotate * localSpeedReserv;
            reserv = Mathf.Clamp(reserv, -maxReserv, maxReserv);
        }
        else if (reserv != 0)
        {
            reserv -= Mathf.Sign(reserv) * speedReserv;
            if (Mathf.Abs(reserv) < 0.02f)
            {
                reserv = 0;
                Delta = 0;
                isWork = false;

                if(GetType() == typeof(RotateToAxixWithLockAngle))
                    print("Work false Rotate input " + isInput + this.GetType().ToString());

                return 0;
            }
        }        
        float rotation = speedRotate * reserv * Multiply();
        // Возвращать дельту между вращением в предыдущем кадре, и этом. Тогда можно будет пользоваться AddTorque

        Delta = rotation - lastRotation;
        lastRotation = rotation;

        //print("Delta: " + Delta +
        //    "\nrotation: " + rotation +
        //    "\nlastRotation: " + lastRotation
        //    );

        return rotation;
    }

    public virtual void InputControle(float nowAxisRow)
    {
        if (Mathf.Abs(oldAxisRow - nowAxisRow) == 1)//Значит мы либо начали движение либо закончили его
        {
            if (oldAxisRow == 0) // Начали движение с нуля
            {
                isInput = true;
                isWork = true;
            }
            else //Закончили
            {
                isInput = false;
            }
        }
        signRotate = nowAxisRow;
        oldAxisRow = nowAxisRow;
    }

    public void MultiplySpeed(float multiply)
    {
        speedRotate *= multiply;
    }

    protected virtual float Multiply() { return 1; }
}
