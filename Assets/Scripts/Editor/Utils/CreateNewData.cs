using Sirenix.OdinInspector;
using System;
using UnityEditor;
using UnityEngine;

public class CreateNewData
{
	public CreateNewData(Type selectedType)
	{
		newSO = ScriptableObject.CreateInstance(selectedType) as XemblemScriptableObject;
		newType = selectedType;
	}

	[InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
	public XemblemScriptableObject newSO;

	private Type newType;

	[Button("Create Scriptable Object")]
	private void CreateNewDataObject()
	{
		string path = "Assets/ScriptableObjects/" + newSO.GetType().Name + "s";
		if (!AssetDatabase.IsValidFolder(path))
		{
			var guid = AssetDatabase.CreateFolder("Assets/ScriptableObjects", newSO.GetType().Name + "s");
		}
		path = path + "/";
		AssetDatabase.CreateAsset(newSO, path + newSO.filename + ".asset");
		AssetDatabase.SaveAssets();

		newSO = ScriptableObject.CreateInstance(newType) as XemblemScriptableObject;
	}
}