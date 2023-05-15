using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class VariableObjectManager : ScriptableObjectManager
{
	public VariableObjectManager()
	{
		typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableVariableAttribute>().OrderBy(m => m.Name).ToArray();
		selectedType = typesToDisplay[0];
		toRemoveForLabel = "Variable";
	}

	[MenuItem("Data Managers/Variables")]
	private static void OpenEditor() => GetWindow<VariableObjectManager>();
}
