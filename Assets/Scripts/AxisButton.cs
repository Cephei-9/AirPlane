using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisButton : MonoBehaviour
{
    [SerializeField] private KeyCode _positiveBatton;
    [SerializeField] private KeyCode _negativeButton;

    public int Value { get; private set; }
    public int value;

    private void Update()
    {
        value = Value;
        if (Input.GetKeyDown(_positiveBatton)) Value = 1;

        if (Input.GetKeyDown(_negativeButton)) Value = -1;

        if (Input.GetKey(_positiveBatton) || Input.GetKey(_negativeButton)) return;
        Value = 0;
    }
}
