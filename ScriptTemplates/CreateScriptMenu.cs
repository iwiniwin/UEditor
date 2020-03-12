using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor.ProjectWindowCallback;
using static UKit.Utils.Output;

public class CreateScriptMenu : MonoBehaviour
{
    private const string MY_SCRIPT_DEFAULT = "Assets/Editor/ScriptTemplates/NewScript.cs.txt";

    [MenuItem("Assets/Create/C# Stand Script", false, 80)]
    public static void CreateMyScript(){
        string locationPath = GetSelectedPathOrFallback();
        string fileName = Path.GetFileNameWithoutExtension(MY_SCRIPT_DEFAULT);
        string fileExtension = Path.GetExtension(fileName).Substring(1);
        Texture2D icon = (EditorGUIUtility.IconContent(fileExtension + " Script Icon").image as Texture2D);
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(), locationPath + "/" + fileName, icon, MY_SCRIPT_DEFAULT);
    }

    public static string GetSelectedPathOrFallback(){
        string path = "Assets";
        foreach(UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets)){
            path = AssetDatabase.GetAssetPath(obj);
            if(!string.IsNullOrEmpty(path) && File.Exists(path)){
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}

class MyDoCreateScriptAsset : EndNameEditAction{
    public override void Action(int instanceId, string pathName, string resourceFile){
        UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(o);
        AssetDatabase.OpenAsset(o.GetInstanceID(), 1);
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile){
        string fullPath = Path.GetFullPath(pathName);
        StreamReader streamReader = new StreamReader(resourceFile);
        string text = streamReader.ReadToEnd();
        streamReader.Close();
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
        // 替换文件名
        text = Regex.Replace(text, "#SCRIPTNAME#", fileNameWithoutExtension);
        bool encoderShouldEmitUTF8Identifier = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
        bool append = false;
        StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
        streamWriter.Write(text);
        streamWriter.Close();
        AssetDatabase.ImportAsset(pathName);
        return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
    }
}

