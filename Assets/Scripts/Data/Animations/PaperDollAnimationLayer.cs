using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FrameInfo
{
	public Sprite AnimationFrame;
	public bool IsBehind;

	public FrameInfo(Sprite frame, bool isBehind)
	{
		AnimationFrame = frame;
		IsBehind = isBehind;
	}
}

public class PaperDollAnimationLayer : XemblemScriptableObject
{
	[SerializeField] bool _manageIsBehinds = false;

	[SerializeField, PreviewField(100, ObjectFieldAlignment.Center), HorizontalGroup("Vert/Zon1", MaxWidth = 250), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	List<Sprite> _downFacingFrames = new List<Sprite>();
	[SerializeField, PreviewField(100, ObjectFieldAlignment.Center), HorizontalGroup("Vert/Zon2", MaxWidth = 250), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	List<Sprite> _upFacingFrames = new List<Sprite>();
	[SerializeField, PreviewField(100, ObjectFieldAlignment.Center), HorizontalGroup("Vert/Zon3", MaxWidth = 250), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	List<Sprite> _rightFacingFrames = new List<Sprite>();
	[SerializeField, PreviewField(100, ObjectFieldAlignment.Center), HorizontalGroup("Vert/Zon4", MaxWidth = 250), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	List<Sprite> _leftFacingFrames = new List<Sprite>();

	[LabelText("Down Behinds"), SerializeField, ShowIf("_manageIsBehinds"), HorizontalGroup("Vert/Zon1", MaxWidth = 200), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	[ValueDropdown("dropdownVals")]
	List<bool> _downFacingIsBehinds = new List<bool>();
	[LabelText("Up Behinds"), SerializeField, ShowIf("_manageIsBehinds"), HorizontalGroup("Vert/Zon2", MaxWidth = 200), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	[ValueDropdown("dropdownVals")]
	List<bool> _upFacingIsBehinds = new List<bool>();
	[LabelText("Right Behinds"), SerializeField, ShowIf("_manageIsBehinds"), HorizontalGroup("Vert/Zon3", MaxWidth = 200), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	[ValueDropdown("dropdownVals")]
	List<bool> _rightFacingIsBehinds = new List<bool>();
	[LabelText("Left Behinds"), SerializeField, ShowIf("_manageIsBehinds"), HorizontalGroup("Vert/Zon4", MaxWidth = 200), VerticalGroup("Vert"), OnValueChanged("Validation"), ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 4)]
	[ValueDropdown("dropdownVals")]
	List<bool> _leftFacingIsBehinds = new List<bool>();

	public bool IsDone
	{
		get
		{
			return _isOneTime && _frame >= _framesInAnimations - 1;
		}
	}



	int _framesInAnimations;
	float _frameRate = 1;
	int _frame = 0;
	float _nextFrameTime = 0;
	bool _isOneTime = false;

	IEnumerable dropdownVals = new ValueDropdownList<bool>()
	{
		{"In Front of Base", false},
		{"Behind Base", true},
	};

	public void Initialize(bool isOneTime, float frameRate)
	{
		_frameRate = frameRate;
		_nextFrameTime = Time.time + (1f / _frameRate);
		_frame = 0;
		_isOneTime = isOneTime;
	}

	public FrameInfo GetFrame(int direction)
	{
		FrameInfo returnVal = new FrameInfo();
		switch (direction)
		{
			case 0:
				returnVal = new FrameInfo(_upFacingFrames[_frame], _upFacingIsBehinds[_frame]);
				break;
			case 1:
				returnVal = new FrameInfo(_rightFacingFrames[_frame], _rightFacingIsBehinds[_frame]);
				break;
			case 2:
				returnVal = new FrameInfo(_downFacingFrames[_frame], _downFacingIsBehinds[_frame]);
				break;
			case 3:
				returnVal = new FrameInfo(_leftFacingFrames[_frame], _leftFacingIsBehinds[_frame]);
				break;
			default:
				break;
		}

		if (_nextFrameTime < Time.time)
		{
			_frame = (_frame < _framesInAnimations - 1) ? _frame + 1 : (IsDone) ? _frame : 0;
			_nextFrameTime = Time.time + (1f / _frameRate);
		}

		return returnVal;
	}

	private void OnValidate()
	{
		Validation();
	}

	private void Validation()
	{
		//this will never return early, purely here to not have a warning appear in the Unity Console
		if (_manageIsBehinds && !_manageIsBehinds) return;
		
		int countU = _upFacingFrames.Count;
		int countL = _leftFacingFrames.Count;
		int countR = _rightFacingFrames.Count;
		int countD = _downFacingFrames.Count;

		_framesInAnimations = Mathf.Max(countD, countL, countR, countU);


		while (_upFacingIsBehinds.Count < _upFacingFrames.Count)
		{
			_upFacingIsBehinds.Add(false);
		}
		while (_downFacingIsBehinds.Count < _downFacingFrames.Count)
		{
			_downFacingIsBehinds.Add(false);
		}
		while (_rightFacingIsBehinds.Count < _rightFacingFrames.Count)
		{
			_rightFacingIsBehinds.Add(false);
		}
		while (_leftFacingIsBehinds.Count < _leftFacingFrames.Count)
		{
			_leftFacingIsBehinds.Add(false);
		}
	}
}
