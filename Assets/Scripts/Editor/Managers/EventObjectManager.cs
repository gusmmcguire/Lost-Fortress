using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EventObjectManager : ScriptableObjectManager
{
	public EventObjectManager()
	{
		typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableEventAttribute>().OrderBy(m => m.Name).ToArray();
		selectedType = typesToDisplay[0];
		toRemoveForLabel = "Object";
	}

	[MenuItem("Data Managers/Events")]
	private static void OpenEditor() => GetWindow<EventObjectManager>();
}
