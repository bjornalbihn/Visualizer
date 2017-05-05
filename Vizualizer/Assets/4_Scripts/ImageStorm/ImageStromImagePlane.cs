using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImageStorm
{
	public class ImageStromImagePlane : MonoBehaviour 
	{
		public bool Active {private set; get;}

		private Renderer _renderer;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
		}

		public void DisplayImage(ImageStormImage image, float speedMultiplier)
		{
			gameObject.SetActive(true);
			Active = true;
			StartCoroutine(ShowImage(image, speedMultiplier));
		}
			
		private IEnumerator ShowImage(ImageStormImage image, float speedMultiplier)
		{
			float duration = image.AppearDuration.Random();
			_renderer.material.mainTexture = image.Texture;
			transform.localRotation = Quaternion.Euler(0,image.RandomRotation.Random(),0);

			float time = 0;
			while (time < 1)
			{
				time = Mathf.Clamp01(time + Time.deltaTime / duration * speedMultiplier);
				_renderer.material.SetColor("_TintColor", Color.white * Mathf.Lerp(0, 1, image.Curve.Evaluate(time)));
				yield return 0;
			}

			Hide();
		}

		public void Hide()
		{
			Active = false;
			gameObject.SetActive(false);
		}

	}
}