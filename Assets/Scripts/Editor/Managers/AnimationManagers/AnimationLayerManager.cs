using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AnimationLayerManager : ScriptableObjectManager
{
	public AnimationLayerManager()
	{
		typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableAnimationLayerAttribute>().OrderBy(m => m.Name).ToArray();
		selectedType = typesToDisplay[0];
		toRemoveForLabel = "Layer";
		sortByCommonString = true;
	}

	[MenuItem("Paper Doll Animations/Animation Layers")]
	private static void OpenEditor() => GetWindow<AnimationLayerManager>();
}
