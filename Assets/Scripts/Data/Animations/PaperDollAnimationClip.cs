using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ManageableAnimationSet]
public class PaperDollAnimationClip : XemblemScriptableObject
{
	[SerializeField, Range(1f,60f)] float _frameRate = 10;
	[SerializeField, TabGroup("Animation Layers")] BehindAnimationLayer _behindLayer;
	[SerializeField, TabGroup("Animation Layers")] BaseAnimationLayer _baseLayer;
	[SerializeField, TabGroup("Animation Layers")] OutfitAnimationLayer _outfitLayer;
	[SerializeField, TabGroup("Animation Layers")] CloakAnimationLayer _cloakLayer;
	[SerializeField, TabGroup("Animation Layers")] FaceAnimationLayer _faceLayer;
	[SerializeField, TabGroup("Animation Layers")] HairAnimationLayer _hairLayer;
	[SerializeField, TabGroup("Animation Layers")] HatAnimationLayer _hatLayer;
	[SerializeField, TabGroup("Animation Layers")] ToolAAnimationLayer _toolALayer;
	[SerializeField, TabGroup("Animation Layers")] ToolBAnimationLayer _toolBLayer;

	[SerializeField, TabGroup("State Transition")] public bool IsOneTime;
	[SerializeField, TabGroup("State Transition"), ShowIf("IsOneTime")] public string TagToTransitionTo;

	[HideInInspector] public FrameInfo CurBehindSprite;
	[HideInInspector] public FrameInfo CurBaseSprite;
	[HideInInspector] public FrameInfo CurOutfitSprite;
	[HideInInspector] public FrameInfo CurCloakSprite;
	[HideInInspector] public FrameInfo CurFaceSprite;
	[HideInInspector] public FrameInfo CurHairSprite;
	[HideInInspector] public FrameInfo CurHatSprite;
	[HideInInspector] public FrameInfo CurToolASprite;
	[HideInInspector] public FrameInfo CurToolBSprite;

	int dir = 2;
	
	public bool IsDone
	{
		get
		{
			return _baseLayer.IsDone;
		}
	}

	public void Initialize()
	{
		CurBehindSprite = new FrameInfo();
		CurBaseSprite = new FrameInfo();
		CurOutfitSprite = new FrameInfo();
		CurCloakSprite = new FrameInfo();
		CurFaceSprite = new FrameInfo();
		CurHairSprite = new FrameInfo();
		CurHatSprite = new FrameInfo();
		CurToolASprite = new FrameInfo();
		CurToolBSprite = new FrameInfo();

		if (_behindLayer != null)
			_behindLayer.Initialize(IsOneTime, _frameRate);
		if (_baseLayer != null)
			_baseLayer.Initialize(IsOneTime, _frameRate);
		if (_outfitLayer != null)
			_outfitLayer.Initialize(IsOneTime, _frameRate);
		if (_cloakLayer != null)
			_cloakLayer.Initialize(IsOneTime, _frameRate);
		if (_faceLayer != null)
			_faceLayer.Initialize(IsOneTime, _frameRate);
		if (_hairLayer != null)
			_hairLayer.Initialize(IsOneTime, _frameRate);
		if (_hatLayer != null)
			_hatLayer.Initialize(IsOneTime, _frameRate);
		if (_toolALayer != null)
			_toolALayer.Initialize(IsOneTime, _frameRate);
		if (_toolBLayer != null)
			_toolBLayer.Initialize(IsOneTime, _frameRate);
	}

	public void SetDirection(int direction)
	{
		if (direction == -1) return;
		else dir = direction;
	}

	public void Animate()
	{
		if (_behindLayer != null)
			CurBehindSprite = _behindLayer.GetFrame(dir);
		if (_baseLayer != null)
			CurBaseSprite = _baseLayer.GetFrame(dir);
		if (_outfitLayer != null)
			CurOutfitSprite = _outfitLayer.GetFrame(dir);
		if (_cloakLayer != null)
			CurCloakSprite = _cloakLayer.GetFrame(dir);
		if (_faceLayer != null)
			CurFaceSprite = _faceLayer.GetFrame(dir);
		if (_hairLayer != null)
			CurHairSprite = _hairLayer.GetFrame(dir);
		if (_hatLayer != null)
			CurHatSprite = _hatLayer.GetFrame(dir);
		if (_toolALayer != null)
			CurToolASprite = _toolALayer.GetFrame(dir);
		if (_toolBLayer != null)
			CurToolBSprite = _toolBLayer.GetFrame(dir);
	}

	private void OnValidate()
	{
		Initialize();
	}
}

