﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
	[CreateAssetMenu(menuName = "ImageStorm/Animated Image")]
	public class ImageStormAnimatedImage : ImageStormSimpleImage
	{
		public AnimationCurve ScaleCurve {get {return _scaleCurve;}}
		public AnimationCurve RotationCurve {get {return _rotationCurve;}}

		[SerializeField] private AnimationCurve _scaleCurve;
		[SerializeField] private AnimationCurve _rotationCurve;

		public override float GetRotationAtTime(float time)
		{
			return _rotationCurve.Evaluate(time);
		}

		public override float GetScaleAtTime(float time)
		{
			return _scaleCurve.Evaluate(time);
		}
	}
}