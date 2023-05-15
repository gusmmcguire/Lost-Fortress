using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class ScriptableObjectManager : OdinMenuEditorWindow
{
	protected Type[] typesToDisplay;
	protected Type selectedType;
	protected string toRemoveForLabel = string.Empty;
	protected bool sortByCommonString = false;
	private CreateNewData createNewData;

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (createNewData != null)
		{
			DestroyImmediate(createNewData.newSO);
		}
	}

	protected override void OnGUI()
	{
		if (typesToDisplay != null)
		{
			if (GUIUtils.SelectButtonList(ref selectedType, typesToDisplay, toRemoveForLabel))
			{
				this.ForceMenuTreeRebuild();
			}
		}
		base.OnGUI();
	}

	protected override OdinMenuTree BuildMenuTree()
	{
		var tree = new OdinMenuTree();

		if (selectedType == null) return tree;

		createNewData = new CreateNewData(selectedType);
		tree.Add("Create New", createNewData);

		tree.AddAllAssetsAtPathOptionalDropdown(GUIUtils.PrettyName(selectedType.Name), "Assets/", selectedType, true, true, sortByCommonString: sortByCommonString);

		return tree;
	}

	protected override void OnBeginDrawEditors()
	{
		if (this.MenuTree == null) return;
		OdinMenuTreeSelection selected = this.MenuTree.Selection;

		if (selectedType == null || selected.SelectedValue == null) return;

		SirenixEditorGUI.BeginHorizontalToolbar();
		{
			GUILayout.FlexibleSpace();
			if (!selected.SelectedValue.GetType().IsAssignableFrom(typeof(CreateNewData)))
			{
				if (SirenixEditorGUI.ToolbarButton("Delete \"" + GUIUtils.PrettyName(((XemblemScriptableObject)selected.SelectedValue).filename) + "\""))
				{
					ScriptableObject asset = selected.SelectedValue as ScriptableObject;
					string path = AssetDatabase.GetAssetPath(asset);
					AssetDatabase.DeleteAsset(path);
					AssetDatabase.SaveAssets();
				}
			}
		}
		SirenixEditorGUI.EndHorizontalToolbar();
	}
}