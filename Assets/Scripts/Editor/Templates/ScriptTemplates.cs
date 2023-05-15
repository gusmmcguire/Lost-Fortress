using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;


internal class ScriptTemplates
{
	private static readonly string _path = "Assets/Scripts/Editor/Templates";

	[MenuItem("Assets/Create Templates/Event",false,0)]
	public static void CreateEventScript() =>
		ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
			ScriptableObject.CreateInstance<DoCreateEventScriptAsset>(),
			"NewEventSO.cs",
			(Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
			$"{_path}/EventSO.txt");

	[MenuItem("Assets/Create Templates/VariableReference", false, 1)]
	public static void CreateReferenceScript() => ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
		ScriptableObject.CreateInstance<DoCreateReferenceScriptAsset>(),
		"NewVariable.cs",
		(Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
		$"{_path}/VariableReference.txt");

	private class DoCreateEventScriptAsset : EndNameEditAction
	{
		public override void Action(int instanceId, string pathName, string resourceFile)
		{
			string text = File.ReadAllText(resourceFile);

			string fileName = Path.GetFileName(pathName);
			{
				string newName = fileName.Replace(" ", "");

				pathName = pathName.Replace(fileName, newName);
				fileName = newName;
			}

			string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
			text = text.Replace("#SCRIPTNAME#", fileNameWithoutExtension);

			string runtimeName = fileNameWithoutExtension.Replace("SO", "");
			text = text.Replace("#RUNTIMENAME#", runtimeName);

			for (int i = runtimeName.Length - 1; i > 0; i--)
				if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
					runtimeName = runtimeName.Insert(i, " ");

			text = text.Replace("#RUNTIMENAME_WITH_SPACES#", runtimeName);

			string fullPath = Path.GetFullPath(pathName);
			var encoding = new UTF8Encoding(true);
			File.WriteAllText(fullPath, text, encoding);
			AssetDatabase.ImportAsset(pathName);
			ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object)));
		}
	}

	private class DoCreateReferenceScriptAsset : EndNameEditAction
	{
		public override void Action(int instanceId, string pathName, string resourceFile)
		{
			string text = File.ReadAllText(resourceFile);

			string fileName = Path.GetFileName(pathName);
			{
				string newName = fileName.Replace(" ", "");

				pathName = pathName.Replace(fileName, newName);
				fileName = newName;
			}

			string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
			text = text.Replace("#RUNTIMENAME#", fileNameWithoutExtension);

			string runtimeName = fileNameWithoutExtension.Replace("Variable", "");
			text = text.Replace("#SCRIPTNAME#", runtimeName);
			
			text = text.Replace("#RUNTIMENAME_LOWERCASE#", runtimeName.ToLower());

			for (int i = runtimeName.Length - 1; i > 0; i--)
				if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
					runtimeName = runtimeName.Insert(i, " ");

			text = text.Replace("#RUNTIMENAME_WITH_SPACES#", runtimeName);

			string fullPath = Path.GetFullPath(pathName);
			var encoding = new UTF8Encoding(true);
			File.WriteAllText(fullPath, text, encoding);
			AssetDatabase.ImportAsset(pathName);
			ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object)));
		}
	}
}
