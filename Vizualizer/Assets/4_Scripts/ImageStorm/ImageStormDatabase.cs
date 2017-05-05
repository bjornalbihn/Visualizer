using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
	[CreateAssetMenu(menuName = "ImageStorm/Database")]
	public class ImageStormDatabase : ScriptableObject 
	{
		[SerializeField] private ImageStormImage[] _images;

		private List<ImageStormImage> _randomImages = new List<ImageStormImage>();
		private System.Random _rnd = new System.Random();

		public List<ImageStormImage> GetRandomImages()
		{
			if (_randomImages.Count != _images.Length)
				UpdateList();
			
			_randomImages.Shuffle(_rnd);
			return _randomImages;
		}

		private void UpdateList()
		{
			_randomImages = new List<ImageStormImage>();
			for (int i = 0; i<_images.Length; i++)
				_randomImages.Add(_images[i]);
		}
	}
}