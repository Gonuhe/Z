using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class PlayerStatusElement:MonoBehaviour
{
    private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            _value = Mathf.Clamp(value, 0, MaxValue);
        }
    }
    public float MaxValue = 100;

    public float DegradationSpeed = 1;

    public static float Degradation_Global_Factor = 0.1f;

    public void Awake()
    {
        Value = MaxValue;
    }

    public void Update()
    {
        Value -= DegradationSpeed * Time.deltaTime * Degradation_Global_Factor;
    }

    public void Restore(float amount)
    {
        Value += amount;
    }
}
