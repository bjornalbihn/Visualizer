using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MinMaxValue
{
    [SerializeField] private float _min;
    [SerializeField] private float _max;

    public MinMaxValue(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public float Min
    {
        get { return _min; }
    }

    public float Max
    {
        get { return _max; }
    }

    public float Random()
    {
        return UnityEngine.Random.Range(_min, _max);
    }
}
