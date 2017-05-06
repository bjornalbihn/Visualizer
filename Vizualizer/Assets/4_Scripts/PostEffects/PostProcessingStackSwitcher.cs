using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingStackSwitcher : MonoBehaviour 
{
	[SerializeField] private PostProcessingBehaviour _postProcessing;
	[SerializeField] private PostProcessingProfile[] _profiles;

	private int _currentProfile;

	private void Awake()
	{
		CycleProfile(0);
	}

	private void Update()
	{
		if (Input.GetKeyDown(InputMapping.PreviousPostEffectKey))
			CycleProfile(-1);
		if (Input.GetKeyDown(InputMapping.NextPostEffectKey))
			CycleProfile(1);
	}

	private void CycleProfile(int direction)
	{
		_currentProfile+= direction;

		if (_currentProfile <0)
			_currentProfile += _profiles.Length;
		
		_currentProfile = _currentProfile % _profiles.Length;

		_postProcessing.profile = _profiles[_currentProfile];
	}
}
