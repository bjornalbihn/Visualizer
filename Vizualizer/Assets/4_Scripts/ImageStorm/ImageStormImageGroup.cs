using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
	public class ImageStormImageGroup : MonoBehaviour 
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

	[Serializable]
	public class ImageStormImage
	{
		public Texture Texture {get {return _texture;}}
		public MinMaxValue AppearDuration {get {return _appearDuration;}}
		public MinMaxValue RandomRotation {get {return _randomRotation;}}
		public AnimationCurve Curve {get {return _curve;}}

		[SerializeField] private Texture _texture;
		[SerializeField] private MinMaxValue _appearDuration;
		[SerializeField] private MinMaxValue _randomRotation;
		[SerializeField] private AnimationCurve _curve;
	}
}