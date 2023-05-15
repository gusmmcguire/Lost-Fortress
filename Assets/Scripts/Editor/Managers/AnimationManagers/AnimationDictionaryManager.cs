using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AnimationDictionaryManager : ScriptableObjectManager
{
	public AnimationDictionaryManager()
	{
		typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableAnimationDictionaryAttribute>().OrderBy(m => m.Name).ToArray();
		selectedType = typesToDisplay[0];
		toRemoveForLabel = "Layer";
		sortByCommonString = true;
	}

	[MenuItem("Paper Doll Animations/Animation Dictionaries")]
	private static void OpenEditor() => GetWindow<AnimationDictionaryManager>();
}
