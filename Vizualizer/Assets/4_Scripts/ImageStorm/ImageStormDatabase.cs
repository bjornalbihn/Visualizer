using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
	[CreateAssetMenu(menuName = "ImageStorm/Database")]
	public class ImageStormDatabase : ScriptableObject 
	{
		[SerializeField] private float _image;
		[SerializeField] private List<ImageStormSimpleImage> _images;

		private List<ImageStormSimpleImage> _randomImages = new List<ImageStormSimpleImage>();
		private System.Random _rnd = new System.Random();

		private void Awake()
		{
			UpdateList();
		}

		public List<ImageStormSimpleImage> GetRandomImages()
		{
			if (_randomImages.Count != _images.Count)
				UpdateList();
			
			_randomImages.Shuffle(_rnd);
			return _randomImages;
		}

		[ContextMenu("UpdateList")]
		private void UpdateList()
		{
			_randomImages = new List<ImageStormSimpleImage>();
			for (int i = 0; i<_images.Count; i++)
				_randomImages.Add(_images[i]);
		}
	}
}