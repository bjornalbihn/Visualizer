using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ImageStorm
{
	public abstract class ImageStormImage : ScriptableObject
	{
		public Texture Texture {get;}
		public float RandomAppearDuration {get;}
		public float RandomRotation {get;}

		public abstract Color GetColorAtTime(float time);
		public abstract float GetRotationAtTime(float time);
		public abstract float GetScaleAtTime(float time);

	}
}