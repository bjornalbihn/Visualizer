using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSync : MonoBehaviour
{
    [Range(-500f, 500f)]
    public float _calibrateTiming = -150;

    private float _lastBeatTime;
    private float _beatDelta;
    private List<float> _taps = new List<float>();
    public static bool Active;
    private int _beatCount = 0;

    public static Action OnBeat;
    public static Action OnOne;
    public static Action OnTwo;
    public static Action OnThree;
    public static Action OnFour;
    public static Action OnOneAndThree;
    public static Action OnTwoAndFour;
    public static Action OnFourBar;

    void Update()
    {
        if (Input.GetKeyDown(InputMapping.BeatSyncKey))
        {
            TapBeat();
        }

        if (Active && Time.time >= _lastBeatTime + _beatDelta + (_calibrateTiming / 1000))
        {
            Beat();
        }
    }

    void Beat(bool firstOrLast = false)
    {
        _beatCount++;

        if (Time.time >= _lastBeatTime + _beatDelta + (_calibrateTiming / 1000))
        {
            if (OnBeat != null)
            {
                OnBeat();
            }
            switch (_beatCount % 4)
            {
                case 1:
                    if (OnOne != null)
                        OnOne();
                    break;
                case 2:
                    if (OnTwo != null)
                        OnTwo();
                    break;
                case 3:
                    if (OnThree != null)
                        OnThree();
                    break;
                case 0:
                    if (OnFour != null)
                        OnFour();
                    break;
            }
            if (_beatCount % 2 == 1 && OnOneAndThree != null)
            {
                OnOneAndThree();
            }
            if (_beatCount % 2 == 0 && OnTwoAndFour != null)
            {
                OnTwoAndFour();
            }
            if (_beatCount % 16 == 1 && OnFourBar != null)
            {
                OnFourBar();
            }
        }

        if (firstOrLast)
            _lastBeatTime = Time.time;
        else
            _lastBeatTime = _lastBeatTime + _beatDelta;
    }

    void TapBeat()
    {
        if (_taps.Count > 0)
        {
            if (Time.time - _taps[_taps.Count - 1] > 1.0f)
            {
                // Long time between taps, assume new tap sequence
                _taps.Clear();
            }
        }
        _taps.Add(Time.time);

        if (_taps.Count > 1)
        {
            Active = true;
            EvaluateBeat();
        }
        else // First or last beat
        {
            Active = false;
            _beatCount = 0;
            Beat(true);
            _beatDelta = 0;
        }
    }

    void EvaluateBeat()
    {
        List<float> tapDeltas = new List<float>();
        for (int i = 1; i < _taps.Count; i++)
        {
            tapDeltas.Add(_taps[i] - _taps[i - 1]);
        }
        float sum = 0;
        foreach (float f in tapDeltas)
        {
            sum += f;
        }
        _beatDelta = sum / tapDeltas.Count;
    }
}
