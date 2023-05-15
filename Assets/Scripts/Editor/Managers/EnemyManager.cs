using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyManager : ScriptableObjectManager
{
	public EnemyManager()
	{
		typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableEnemyAttribute>().OrderBy(m => m.Name).ToArray();
		selectedType = typesToDisplay[0];
	}

	[MenuItem("Data Managers/Enemies")]
	private static void OpenEditor() => GetWindow<EnemyManager>();
}
