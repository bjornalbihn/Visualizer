using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImageStorm
{
	public class ImageStorm : MonoBehaviour 
	{
		[SerializeField] private ImageStormDatabase _database;
		[SerializeField] private MinMaxValue _delayBetweenImages;
		[SerializeField] private float _showSpeedMultiplier = 1;
		[SerializeField] private float _switchSpeedMultiplier = 1;

		private List<ImageStormSimpleImage> _images;
		private ImageStromImagePlane[] _planes;

		// Use this for initialization
		void Awake () 
		{
			_planes = GetComponentsInChildren<ImageStromImagePlane>(true);
		}

		void OnEnable()
		{
			for (int i = 0; i< _planes.Length; i++)
				_planes[i].Hide();
			
			StartCoroutine(ShowImages());
		}

		private IEnumerator ShowImages()
		{
			while (true)
			{
				int currentPlane = 0;
				_images = _database.GetRandomImages();
				for (int i = 0; i<_images.Count; i++)
				{
					currentPlane = (currentPlane + 1) % _planes.Length;
					if (_planes[currentPlane].Active)
						yield return 0;
					else
						_planes[currentPlane].DisplayImage(_images[i], _showSpeedMultiplier);

					yield return new WaitForSeconds(_delayBetweenImages.Random() * _switchSpeedMultiplier);
				}
			}
		}		 
	}
}