using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InputObjectManager : ScriptableObjectManager
{
	public InputObjectManager()
	{
		selectedType = typeof(InputLayer);
	}

	[MenuItem("Data Managers/Input")]
	private static void OpenEditor() => GetWindow<InputObjectManager>();
}
