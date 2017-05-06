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

		public void DisplayImage(ImageStormSimpleImage image, float speedMultiplier)
		{
			gameObject.SetActive(true);
			Active = true;
			StartCoroutine(ShowImage(image, speedMultiplier));
		}
			
		private IEnumerator ShowImage(ImageStormSimpleImage image, float speedMultiplier)
		{
			float duration = image.RandomAppearDuration;
			float rotation = image.RandomRotation;
			float scale = image.RandomScale;
			_renderer.material.mainTexture = image.Texture;

			float time = 0;
			while (time < 1)
			{
				time = Mathf.Clamp01(time + Time.deltaTime / duration * speedMultiplier);
				_renderer.material.SetColor("_TintColor", image.GetColorAtTime(time));

				transform.localRotation = Quaternion.Euler(0,image.GetRotationAtTime(time)*rotation,0);
				transform.localScale = Vector3.one * image.GetScaleAtTime(time) * scale;

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