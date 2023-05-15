using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManageableAnimationDictionaryAttribute]
public class PaperDollAnimationDictionary : XemblemScriptableObject
{
	[SerializeField]
	StringAnimationLayerSetDictionary _animations;

	public bool TryGetAnimationByTag(string tag, out PaperDollAnimationClip output)
	{
		if (_animations.ContainsKey(tag))
		{
			output = _animations[tag];
			return true;
		}
		output = null;
		return false;
	}
}
