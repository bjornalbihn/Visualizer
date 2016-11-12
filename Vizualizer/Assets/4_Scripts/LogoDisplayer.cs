using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogoDisplayer : MonoBehaviour {

    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Texture[] _logos;
    [SerializeField] private AnimationCurve _flashCurve;
    [SerializeField] private AnimationCurve _intensityCurve;
    [SerializeField] private float _flashDuration = 0.4f;
    [SerializeField] private EffectLayer _effectLayer;

    [SerializeField] private Color[] _randomColor;
    [SerializeField] private bool _useRandomColor;

	[SerializeField] private int _affectedByChannel = 4;


    void Start()
    {
        _effectLayer.OnEffectFired += EvaluateEffect;

        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.SetColor("_TintColor", Color.clear);
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.SetColor("_TintColor", Color.clear);
        }
    }

    void EvaluateEffect(int id)
    {
		if (id == _affectedByChannel)
        {
            ChangeLogos();
        }
    }

    void ChangeLogos()
    {
        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.SetColor("_TintColor", Color.clear);
        }

        List<Texture> logos = new List<Texture>();

        foreach (Texture t in _logos)
        {
            bool logoUsed = false;
            foreach (MeshRenderer mr in _renderers)
            {
                if (mr.material.mainTexture == t)
                    logoUsed = true;
            }

            if (!logoUsed)
                logos.Add(t);
        }

        foreach (MeshRenderer mr in _renderers)
        {
            int random = Random.Range(0, logos.Count);
            Debug.Log("Picked index " + random);
            mr.material.mainTexture = logos[random];
            mr.material.SetTexture("_MainTex", logos[random]);
            logos.Remove(logos[random]);
            Debug.Log("Number of textures remaining: " +logos.Count);
        }
        StopAllCoroutines();
        StartCoroutine(FlashLogos());
    }

    IEnumerator FlashLogos()
    {
        Color color = Color.white;

        if (_useRandomColor)
            color = _randomColor[Random.Range(0, _randomColor.Length)];

        float timer = _flashDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(timer, 0);

            foreach (MeshRenderer mr in _renderers)
            {
                float intensity = _intensityCurve.Evaluate(timer / _flashDuration);
                Color currentColor = color * intensity;
                currentColor.a = _flashCurve.Evaluate(timer / _flashDuration);
                mr.material.SetColor("_TintColor", currentColor);
            }
            yield return 0;
        }

        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.color = new Color(1, 1, 1, 0);
        }
    }
}
