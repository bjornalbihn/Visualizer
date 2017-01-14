using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HexagonController : MonoBehaviour
{
    [SerializeField] private Hexagon _root;
    [SerializeField] private float _delay;

    [SerializeField] private Color[] _colors;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            Color color = _colors[Random.Range(0,_colors.Length)];
            StartCoroutine(ChangeColor(_root, color));
        }
    }

    private IEnumerator ChangeColor(Hexagon hexagon, Color targetColor)
    {
        hexagon.SetColor(targetColor);
        yield return new WaitForSeconds(_delay);

        foreach (var hex in hexagon.Children)
        {
            StartCoroutine(ChangeColor(hex, targetColor));
        }
    }

}
