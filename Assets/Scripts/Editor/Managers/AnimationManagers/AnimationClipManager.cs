using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AnimationClipManager : ScriptableObjectManager
{
	public AnimationClipManager()
	{
		typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableAnimationSetAttribute>().OrderBy(m => m.Name).ToArray();
		selectedType = typesToDisplay[0];
		toRemoveForLabel = "Layer";
		sortByCommonString = true;
	}

	[MenuItem("Paper Doll Animations/Animation Clips")]
	private static void OpenEditor() => GetWindow<AnimationClipManager>();
}
