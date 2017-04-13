using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PostEffects/GlitchValues")]
public class GlitchEffectValues : ScriptableObject 
{
	public bool Debug {get {return _debug;}}
	public GlitchSetting DebugSetting {get {return _debugSetting;}}
	public GlitchSetting CurrentSetting {get {return _settings[_currentSetting];}}

	[SerializeField] private List<GlitchSetting> _settings;
	private int _currentSetting;

	[Header("Debug")]
	[SerializeField] private bool _debug;
	[SerializeField] private GlitchSetting _debugSetting;

	private void Awake()
	{
		_currentSetting = 0;
	}

	public GlitchSetting Cycle()
	{
		_currentSetting = (_currentSetting + 1) % _settings.Count;
		return CurrentSetting;
	}

}

[Serializable]
public class GlitchSetting
{
	[Range(0, 10)] public float VerticalJerk;
	[Range(0, 10)] public float VerticalMovement;
	[Range(0, 10)] public float BottomStatic;
	[Range(0, 10)] public float Scanlines;
	[Range(0, 10)] public float RgbOffset;
	[Range(0, 10)] public float HorziontalFuzz;
}
