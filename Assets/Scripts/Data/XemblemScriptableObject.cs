using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class XemblemScriptableObject : ScriptableObject
{
	[InlineButton("RenameFile", label: "Rename File", icon: SdfIconType.Save), OnInspectorDispose("OnDispose")]
	public string filename;


	private void OnDispose()
	{
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		string assetName = assetPath.Substring(assetPath.LastIndexOf('/') + 1).Replace(".asset", string.Empty);
		filename = assetName;
	}

	private void RenameFile()
	{
		string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
		AssetDatabase.RenameAsset(assetPath, filename);
		AssetDatabase.SaveAssets();
	}
}
