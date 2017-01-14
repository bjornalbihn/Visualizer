using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Hexagon : MonoBehaviour
{
    public Hexagon[] Children {  get { return _children; }}

    [SerializeField] private Hexagon[] _children;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _duration;
    [SerializeField] private bool _goToWhiteFirst;

    private Color _color;

    public void SetColor(Color targetColor)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColor(targetColor));
    }


    private IEnumerator ChangeColor(Color targetColor)
    {
        float time = 0;
        if (_goToWhiteFirst)
        {
            while (time < 1)
            {
                time = Mathf.Clamp01(time + Time.deltaTime / _duration);
                SetNewColor(Color.Lerp(_color, Color.white, _curve.Evaluate(time)));
                yield return null;
            }
            time = 0;
        }
        while (time < 1)
        {
            time = Mathf.Clamp01(time + Time.deltaTime / _duration);
            SetNewColor(Color.Lerp(_color,targetColor, _curve.Evaluate(time)));
            yield return null;
        }
    }

    private void SetNewColor(Color color)
    {
        _color = color;
        GetComponent<Renderer>().material.color = color;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Hexagon hex in _children)
        {
            Gizmos.DrawLine(transform.position, hex.transform.position);
        }
    }
}
