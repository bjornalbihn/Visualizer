using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogoDisplayer : MonoBehaviour {

    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Texture[] _logos;
    [SerializeField] private AnimationCurve _flashCurve;
    [SerializeField] private float _flashDuration = 0.4f;
    [SerializeField] private EffectLayer _effectLayer;

    void Start()
    {
        _effectLayer.OnEffectFired += EvaluateEffect;

        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.color = new Color(1, 1, 1, 0);
        }
    }

    void EvaluateEffect(int id)
    {
        if (id == 4)
        {
            ChangeLogos();
        }
    }

    void ChangeLogos()
    {
        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.color = new Color(1, 1, 1, 0);
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
            mr.material.SetTexture("_EmissionMap", logos[random]);
            logos.Remove(logos[random]);
            Debug.Log("Number of textures remaining: " +logos.Count);
        }
        StopAllCoroutines();
        StartCoroutine(FlashLogos());
    }

    IEnumerator FlashLogos()
    {
        float timer = _flashDuration;
        while (timer > 0)
        {
            foreach(MeshRenderer mr in _renderers)
            {
                mr.material.color = new Color(1, 1, 1, _flashCurve.Evaluate(timer / _flashDuration));
            }
            timer -= Time.deltaTime;
            yield return 0;
        }

        foreach (MeshRenderer mr in _renderers)
        {
            mr.material.color = new Color(1, 1, 1, 0);
        }
    }
}
