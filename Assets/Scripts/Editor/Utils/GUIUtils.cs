using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class GUIUtils
{
	public static bool SelectButtonList(ref Type selectedType, Type[] typesToDisplay, string removeFromName = "")
	{
		var rect = GUILayoutUtility.GetRect(0, 25);
		for (int i = 0; i < typesToDisplay.Length; i++)
		{
			var name = PrettyName(typesToDisplay[i].Name, true, removeFromName);
			var btnRect = rect.Split(i, typesToDisplay.Length);
			if (GUIUtils.SelectButton(btnRect, name, typesToDisplay[i] == selectedType))
			{
				selectedType = typesToDisplay[i];
				return true;
			}
		}

		return false;
	}


	public static bool SelectButton(Rect rect, string name, bool selected)
	{
		if (GUI.Button(rect, GUIContent.none, GUIStyle.none)) return true;

		if (Event.current.type == EventType.Repaint)
		{
			var style = new GUIStyle(EditorStyles.miniButtonMid);
			style.stretchHeight = true;
			style.fixedHeight = rect.height;
			style.Draw(rect, GUIHelper.TempContent(name), false, false, selected, false);
		}

		return false;
	}

	public static string PrettyName(string name, bool addPlural = false, string removeValue = "")
	{
		string result = addPlural ? name + "s" : name;
		if (addPlural && result[result.Length - 2] == 'y') result = result.Substring(0, result.Length - 2) + "ies";
		if (removeValue == "") return AddSpacesAtCapitals(result);
		return AddSpacesAtCapitals(result.Replace(removeValue, string.Empty));
	}

	private static string AddSpacesAtCapitals(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
			return "";
		StringBuilder newText = new StringBuilder(text.Length * 2);
		newText.Append(text[0]);
		for (int i = 1; i < text.Length; i++)
		{
			if (char.IsUpper(text[i]) && text[i - 1] != ' ')
				newText.Append(' ');
			newText.Append(text[i]);
		}
		return newText.ToString();
	}


	#region Taken From Odin and Repurposed
	//NEVER OPEN ITS UGLY (IT TRULY IS)
	public static IEnumerable<OdinMenuItem> AddAllAssetsAtPathOptionalDropdown(this OdinMenuTree tree, string menuPath, string assetFolderPath, Type type, bool includeSubDirectories = false, bool flattenSubDirectories = false, bool sortByCommonString = false)
	{
		assetFolderPath = (assetFolderPath ?? "").TrimEnd(new char[1] { '/' }) + "/";
		string loweredAssetFolderPath = assetFolderPath.ToLower();
		if (!loweredAssetFolderPath.StartsWith("assets/") && !loweredAssetFolderPath.StartsWith("packages/"))
		{
			assetFolderPath = "Assets/" + assetFolderPath;
		}

		assetFolderPath = assetFolderPath.TrimEnd(new char[1] { '/' }) + "/";
		IEnumerable<string> enumerable = from x in AssetDatabase.GetAllAssetPaths()
										 where includeSubDirectories ? x.StartsWith(assetFolderPath, StringComparison.InvariantCultureIgnoreCase) : (string.Compare(PathUtilities.GetDirectoryName(x).Trim(new char[1] { '/' }), assetFolderPath.Trim(new char[1] { '/' }), ignoreCase: true) == 0)
										 select x;
		menuPath = menuPath ?? "";
		menuPath = menuPath.TrimStart(new char[1] { '/' });
		HashSet<OdinMenuItem> result = new HashSet<OdinMenuItem>();



		foreach (string item in enumerable)
		{
			UnityEngine.Object @object = AssetDatabase.LoadAssetAtPath(item, type);
			if (@object == null)
			{
				continue;
			}

			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
			string savedMenuPath = menuPath;
			if (!flattenSubDirectories)
			{
				string directoryName = PathUtilities.GetDirectoryName(item).TrimEnd(new char[1] { '/' }) + "/";
				directoryName = directoryName.Substring(assetFolderPath.Length);
				if (directoryName.Length != 0)
				{
					savedMenuPath = savedMenuPath.Trim(new char[1] { '/' }) + "/" + directoryName;
				}
			}

			savedMenuPath = savedMenuPath.Trim(new char[1] { '/' }) + "/" + fileNameWithoutExtension;
			SplitMenuPath(savedMenuPath, out savedMenuPath, out var name);

			if (sortByCommonString)
			{
				string pattern = "([A-Z])\\w+_";
				Regex rg = new Regex(pattern);
				string dropdownPath = (rg.Match(name).Value).Replace("_", string.Empty) + "/" + name;
				tree.Add(dropdownPath, @object);
			}
			else
			{
				tree.Add(name, @object);
			}
		}

		return result;
	}

	private static void SplitMenuPath(string menuPath, out string path, out string name)
	{
		menuPath = menuPath.Trim(new char[1] { '/' });
		int num = menuPath.LastIndexOf('/');
		if (num == -1)
		{
			path = "";
			name = menuPath;
		}
		else
		{
			path = menuPath.Substring(0, num);
			name = menuPath.Substring(num + 1);
		}
	}
	#endregion
}
