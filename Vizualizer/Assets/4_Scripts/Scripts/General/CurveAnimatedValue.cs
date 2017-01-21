using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CurveAnimatedValue
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _duration;

    public float Value {private set; get;}
    public Action<float> OnValue;

    public IEnumerator SetNewValue(bool forward, float multiplier = 1)
    {
        float start = forward ? 0 : 1;
        float end = forward ? 1 : 0;

        float time = 0;
        while (time < 1)
        {
            time = Mathf.Clamp01(time + Time.deltaTime / _duration * multiplier);
            Value = Mathf.Lerp(start, end, _curve.Evaluate(time));
            if (OnValue != null)
                OnValue(Value);

            yield return 0;
        }
    }
}
