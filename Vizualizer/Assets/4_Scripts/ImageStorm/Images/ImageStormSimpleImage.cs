using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
	[CreateAssetMenu(menuName = "ImageStorm/Simple Image")]
	public class ImageStormSimpleImage : ImageStormImage
	{
		public Texture Texture {get {return _texture;}}
		public float RandomAppearDuration {get {return _appearDuration.Random();}}
		public float RandomRotation {get {return _randomRotation.Random();}}

		[SerializeField] private Texture _texture;
		[SerializeField] private MinMaxValue _appearDuration;
		[SerializeField] protected MinMaxValue _randomRotation;
		[SerializeField] private AnimationCurve _curve;

		public override Color GetColorAtTime(float time)
		{
			return Color.white * _curve.Evaluate(time);
		}
			
		public override float GetRotationAtTime(float time)
		{
			return 1;
		}

		public override float GetScaleAtTime(float time)
		{
			return 1;
		}
	}
}