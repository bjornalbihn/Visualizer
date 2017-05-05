using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
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

	[CreateAssetMenu(menuName = "ImageStorm/Simple Image")]
	public class ImageStormSimpleImage : ScriptableObject
	{
		public Texture Texture {get {return _texture;}}
		public MinMaxValue AppearDuration {get {return _appearDuration;}}
		public MinMaxValue RandomRotation {get {return _randomRotation;}}
		public AnimationCurve Curve {get {return _curve;}}

		[SerializeField] private Texture _texture;
		[SerializeField] private MinMaxValue _appearDuration;
		[SerializeField] private MinMaxValue _randomRotation;
		[SerializeField] private AnimationCurve _curve;



		[SerializeField] private ImageStormImageGroup _group;
		[SerializeField] private int _int;

		[ContextMenu("Convert")]
		private void Convert()
		{
			ImageStormImage image = _group._images[_int];

			_texture = image.Texture;
			_curve = image.Curve;
			_appearDuration = image.AppearDuration;
			_randomRotation = image.RandomRotation;

			name = image.Texture.name;
		}
	}
}