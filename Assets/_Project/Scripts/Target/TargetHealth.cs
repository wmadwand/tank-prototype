using UnityEngine;

public interface IEntityiHealth
{
    float Max { get; }
    float Value { get; }

    void Add(float value);
    void Remove(float value);
}

public class TargetHealth : IEntityiHealth
{
    public float Value { get; private set; }
    public float Max { get; private set; }

    public TargetHealth(float max)
    {
        Value = max;
        Max = max;
    }

    public void Add(float value)
    {
        Value = Mathf.Clamp(Value + value, 0, Max);
    }

    public void Remove(float value)
    {
        if (Value <= 0) { return; }

        Value -= value;
    }
}
