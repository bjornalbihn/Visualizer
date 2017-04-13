using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "PostEffects/MirrorValues")]
public class MirrorValues : ScriptableObject 
{
	public bool Debug {get {return _debug;}}
	public MirrorSetting DebugSetting {get {return _debugSetting;}}
	public MirrorSetting CurrentSetting {get {return _settings[_currentSetting];}}

	[SerializeField] private List<MirrorSetting> _settings;
	private int _currentSetting;

	[Header("Debug")]
	[SerializeField] private bool _debug;
	[SerializeField] private MirrorSetting _debugSetting;

	private void Awake()
	{
		SetSetting(0);
	}

	public MirrorSetting SetSetting(int setting)
	{
		_currentSetting =  (setting) % _settings.Count;
		return CurrentSetting;
	}
}

[Serializable]
public class MirrorSetting
{
	[Range(0, 1)] public float Horizontal;
	[Range(0, 1)] public float Vertical;
}

